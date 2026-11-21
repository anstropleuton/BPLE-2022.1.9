using System;
using System.Collections.Generic;
using System.IO;
using HybridCLR.Editor;
using HybridCLR.Editor.Commands;
using HybridCLR.Editor.Installer;
using HybridCLR.Editor.Settings;
using Innovation.Script;
using UnityEditor;
using UnityEngine;

public static class HybridCLRBuildSupport
{
	private const string HybridCLRRepoUrl = "https://github.com/focus-creative-games/hybridclr";

	private const string Il2CppPlusRepoUrl = "https://github.com/focus-creative-games/il2cpp_plus";

	private static readonly string[] FrameworkDirCandidates =
	{
		"MonoBleedingEdge/lib/mono/4.7.1-api",
		"MonoBleedingEdge/lib/mono/4.8-api",
		"MonoBleedingEdge/lib/mono/4.5-api"
	};

	public static string ReferenceStagePath =>
		Path.Combine(Orchestrate.ProjectPath, "Assets", "StreamingAssets", ScriptReferenceSet.ReferenceFolderName);

	public static string MetadataStagePath =>
		Path.Combine(Orchestrate.ProjectPath, "Assets", "StreamingAssets", ScriptReferenceSet.MetadataFolderName);

	[MenuItem("BPLE/HybridCLR/Setup")]
	public static void Setup()
	{
		HybridCLRSettings settings = HybridCLRSettings.Instance;
		settings.enable = true;
		settings.useGlobalIl2cpp = false;
		settings.hybridclrRepoURL = HybridCLRRepoUrl;
		settings.il2cppPlusRepoURL = Il2CppPlusRepoUrl;
		settings.outputAOTGenericReferenceFile = "../HybridCLRData/Generated/AOTGenericReferences.cs";
		HybridCLRSettings.Save();

		InstallerController installer = new InstallerController();
		if (installer.HasInstalledHybridCLR() && installer.PackageVersion == installer.InstalledLibil2cppVersion)
		{
			Debug.Log("[HybridCLR] Already installed (" + installer.PackageVersion + "). Settings saved.");
			return;
		}

		Debug.Log("[HybridCLR] Installing (needs git on PATH; clones il2cpp_plus, this can take a few minutes)...");
		installer.InstallDefaultHybridCLR();
		Debug.Log("[HybridCLR] Install finished.");
	}

	public static void PrepareForBuild(BuildTarget target)
	{
		CleanStaging();

		InstallerController installer = new InstallerController();
		if (!installer.HasInstalledHybridCLR() || installer.PackageVersion != installer.InstalledLibil2cppVersion)
		{
			throw new Exception("HybridCLR is not installed (or the package was updated). Run the menu BPLE > HybridCLR > Setup (run once) first.");
		}

		if (!File.Exists(Path.Combine(Orchestrate.ProjectPath, "Assets", "link.xml")))
		{
			throw new Exception("Assets/link.xml is missing. It keeps whole assemblies unstripped so runtime scripts can call any API.");
		}

		PrebuildCommand.GenerateAll();

		StageMetadata(target);
		StageReferences(target);
	}

	[MenuItem("BPLE/HybridCLR/Clear")]
	public static void CleanStaging()
	{
		DeleteDirectoryAndMeta(ReferenceStagePath);
		DeleteDirectoryAndMeta(MetadataStagePath);
	}

	private static void StageMetadata(BuildTarget target)
	{
		string sourceDir = MakeAbsolute(SettingsUtil.GetAssembliesPostIl2CppStripDir(target));
		if (!Directory.Exists(sourceDir))
		{
			throw new Exception("HybridCLR did not produce stripped AOT assemblies at: " + sourceDir);
		}

		Directory.CreateDirectory(MetadataStagePath);
		int copied = 0;
		foreach (string name in ScriptReferenceSet.MetadataAssemblyNames)
		{
			string source = Path.Combine(sourceDir, name + ".dll");
			if (!File.Exists(source))
			{
				if (name == "mscorlib" || name == ScriptReferenceSet.HostAssemblyName)
				{
					throw new Exception("Required stripped AOT assembly is missing: " + source);
				}
				Debug.LogWarning("[HybridCLR] Stripped AOT assembly not found, no supplementary metadata for it: " + name);
				continue;
			}
			File.Copy(source, Path.Combine(MetadataStagePath, name + ".dll"), true);
			copied++;
		}
		Debug.Log("[HybridCLR] Staged supplementary metadata: " + copied + " assemblies -> " + MetadataStagePath);
	}

	private static void StageReferences(BuildTarget target)
	{
		string contents = EditorApplication.applicationContentsPath;
		string frameworkDir = FindFrameworkDir(contents);
		string unityModulesDir = Path.Combine(Path.Combine(contents, "Managed"), "UnityEngine");

		Directory.CreateDirectory(ReferenceStagePath);
		int copied = 0;

		string playerDir = MakeAbsolute(SettingsUtil.GetAssembliesPostIl2CppStripDir(target));

		foreach (string name in ScriptReferenceSet.FrameworkAssemblyNames)
		{
			string source = FirstExisting(
				Path.Combine(playerDir, name + ".dll"),
				Path.Combine(frameworkDir, name + ".dll"),
				Path.Combine(Path.Combine(frameworkDir, "Facades"), name + ".dll"));
			if (source == null)
			{
				continue;
			}
			CopyReference(source, name);
			copied++;
		}

		foreach (string name in ScriptReferenceSet.UnityModuleNames)
		{
			CopyReference(RequireFile(Path.Combine(unityModulesDir, name + ".dll")), name);
			copied++;
		}

		CopyReference(RequireFile(Path.Combine(Orchestrate.ProjectPath, "Assets", "Plugins", ScriptReferenceSet.UnityUIAssemblyName + ".dll")), ScriptReferenceSet.UnityUIAssemblyName);
		copied++;

		string hostDir = MakeAbsolute(SettingsUtil.GetHotUpdateDllsOutputDirByTarget(target));
		CopyReference(RequireFile(Path.Combine(hostDir, ScriptReferenceSet.HostAssemblyName + ".dll")), ScriptReferenceSet.HostAssemblyName);
		copied++;

		Debug.Log("[HybridCLR] Staged script reference assemblies: " + copied + " -> " + ReferenceStagePath);
	}

	private static string FindFrameworkDir(string contents)
	{
		List<string> tried = new List<string>();
		foreach (string candidate in FrameworkDirCandidates)
		{
			string dir = Path.Combine(contents, candidate);
			tried.Add(dir);
			if (File.Exists(Path.Combine(dir, "mscorlib.dll")))
			{
				return dir;
			}
		}
		throw new Exception("Could not find the .NET reference assemblies inside the Unity Editor. Looked in:\n" + string.Join("\n", tried.ToArray()) +
			"\nOpen the Editor's MonoBleedingEdge/lib/mono folder, find the folder containing mscorlib.dll plus a Facades folder, and add it to FrameworkDirCandidates in HybridCLRBuildSupport.cs.");
	}

	private static void CopyReference(string source, string assemblyName)
	{
		File.Copy(source, Path.Combine(ReferenceStagePath, assemblyName + ".dll"), true);
	}

	private static string RequireFile(string path)
	{
		if (!File.Exists(path))
		{
			throw new Exception("Required file for script references not found: " + path);
		}
		return path;
	}

	private static string FirstExisting(params string[] paths)
	{
		foreach (string path in paths)
		{
			if (File.Exists(path))
			{
				return path;
			}
		}
		return null;
	}

	private static string MakeAbsolute(string path)
	{
		return Path.IsPathRooted(path) ? path : Path.Combine(Orchestrate.ProjectPath, path);
	}

	private static void DeleteDirectoryAndMeta(string path)
	{
		if (Directory.Exists(path))
		{
			Directory.Delete(path, true);
		}
		if (File.Exists(path + ".meta"))
		{
			File.Delete(path + ".meta");
		}
	}
}
