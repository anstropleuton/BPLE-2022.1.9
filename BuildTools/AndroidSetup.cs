#!/usr/bin/env -S dotnet --
#:property TargetFramework=net11.0

using System.Diagnostics;
using System.Formats.Tar;
using System.IO.Compression;

if (args is not ["--root", string rootArgument, "--api", string platformApi])
{
    Console.Error.WriteLine("Usage: dotnet run AndroidSetup.cs -- --root <directory> --api <android-NN>");
    return 1;
}

string rootDirectory = Path.GetFullPath(rootArgument);
string jdkDirectory = Path.Combine(rootDirectory, "jdk");
string sdkDirectory = Path.Combine(rootDirectory, "sdk");
string commandLineToolsDirectory = Path.Combine(sdkDirectory, "cmdline-tools");
string latestToolsDirectory = Path.Combine(commandLineToolsDirectory, "latest");
bool isWindows = OperatingSystem.IsWindows();
string operatingSystem = isWindows ? "windows" : "linux";
using HttpClient httpClient = new() { Timeout = TimeSpan.FromMinutes(30) };

string? FindJava() => Directory.Exists(jdkDirectory)
    ? Directory.EnumerateFiles(jdkDirectory, isWindows ? "java.exe" : "java", SearchOption.AllDirectories).FirstOrDefault()
    : null;

if (FindJava() is null)
{
    await DownloadAndExtractAsync($"https://api.adoptium.net/v3/binary/version/jdk-11.0.14.1%2B1/{operatingSystem}/x64/jdk/hotspot/normal/eclipse", jdkDirectory, isZip: isWindows);
}
string javaExecutable = FindJava()!;
string javaHome = Path.GetDirectoryName(Path.GetDirectoryName(javaExecutable))!;

if (!Directory.Exists(latestToolsDirectory))
{
    string platform = isWindows ? "win" : "linux";
    await DownloadAndExtractAsync($"https://dl.google.com/android/repository/commandlinetools-{platform}-9123335_latest.zip", commandLineToolsDirectory, isZip: true);
    Directory.Move(Path.Combine(commandLineToolsDirectory, "cmdline-tools"), latestToolsDirectory);
}

string licenseAnswers = Path.GetTempFileName();
await File.WriteAllTextAsync(licenseAnswers, string.Concat(Enumerable.Repeat("y\n", 40)));
await RunSdkManagerAsync("--licenses");
await RunSdkManagerAsync("build-tools;34.0.0", "ndk;21.3.6528147", "platform-tools", $"platforms;{platformApi}");
File.Delete(licenseAnswers);

Console.WriteLine($"""
    Unity Preferences > External Tools > Android (untick "installed with Unity"):
      JDK : {javaHome}
      SDK : {sdkDirectory}
      NDK : {Path.Combine(sdkDirectory, "ndk", "21.3.6528147")}
    """);
return 0;

async Task DownloadAndExtractAsync(string url, string destinationDirectory, bool isZip)
{
    Console.WriteLine($"Downloading {url}");
    await using Stream download = await httpClient.GetStreamAsync(url);
    if (isZip) await ZipFile.ExtractToDirectoryAsync(download, destinationDirectory, overwriteFiles: true);
    else
    {
        await using GZipStream gzip = new(download, CompressionMode.Decompress);
        await TarFile.ExtractToDirectoryAsync(gzip, destinationDirectory, overwriteFiles: true);
    }
}

async Task RunSdkManagerAsync(params string[] arguments)
{
    using var input = File.OpenHandle(licenseAnswers);
    ProcessStartInfo startInfo = new(javaExecutable)
    {
        StandardInputHandle = input,
        ArgumentList =
        {
            $"-Dcom.android.sdklib.toolsdir={latestToolsDirectory}",
            "-classpath", Path.Combine(latestToolsDirectory, "lib", "*"),
            "com.android.sdklib.tool.sdkmanager.SdkManagerCli",
            $"--sdk_root={sdkDirectory}",
        },
    };
    foreach (string argument in arguments) startInfo.ArgumentList.Add(argument);

    var status = await Process.RunAsync(startInfo);
    if (status.ExitCode != 0) throw new InvalidOperationException($"sdkmanager exited with code {status.ExitCode}.");
}