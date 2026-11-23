using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Innovation.Script
{
	public static class ScriptReferenceSet
	{
		public const string ReferenceFolderName = "ScriptReferences";

		public const string MetadataFolderName = "HybridCLRMetadata";

		public const string HostAssemblyName = "Assembly-CSharp";

		public const string UnityUIAssemblyName = "UnityEngine.UI";

		public static readonly string[] FrameworkAssemblyNames =
		{
			"mscorlib", "netstandard", "System.Private.CoreLib", "System", "System.Collections",
			"System.Collections.Concurrent", "System.Console", "System.Diagnostics.Debug",
			"System.Diagnostics.Process", "System.Diagnostics.StackTrace", "System.Globalization",
			"System.IO", "System.IO.FileSystem", "System.IO.FileSystem.Primitives", "System.Reflection",
			"System.Reflection.Extensions", "System.Reflection.Primitives", "System.Runtime",
			"System.Runtime.Extensions", "System.Runtime.InteropServices", "System.Text.Encoding",
			"System.Text.Encoding.CodePages", "System.Text.Encoding.Extensions",
			"System.Text.RegularExpressions", "System.Threading", "System.Threading.Tasks",
			"System.Threading.Tasks.Parallel", "System.Threading.Thread", "System.ValueTuple"
		};

		public static readonly string[] UnityModuleNames =
		{
			"UnityEngine.AndroidJNIModule", "UnityEngine.AssetBundleModule", "UnityEngine.AudioModule",
			"UnityEngine.CoreModule", "UnityEngine.PhysicsModule", "UnityEngine.UIModule",
			"UnityEngine.UnityWebRequestModule", "UnityEngine.VideoModule"
		};

		public static readonly string[] MetadataAssemblyNames =
		{
			"mscorlib", "System", "System.Core", "UnityEngine.CoreModule", "Newtonsoft.Json", HostAssemblyName
		};

		public static string GetStreamingFilePath(string folderName, string assemblyName)
		{
			return Application.streamingAssetsPath + "/" + folderName + "/" + assemblyName + ".dll";
		}

		public static byte[] ReadStreamingFile(string path)
		{
			try
			{
				if (!path.Contains("://"))
				{
					return File.Exists(path) ? File.ReadAllBytes(path) : null;
				}

				using (UnityWebRequest request = UnityWebRequest.Get(path))
				{
					request.SendWebRequest();
					System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
					while (!request.isDone)
					{
						if (stopwatch.ElapsedMilliseconds > 15000)
						{
							request.Abort();
							Debug.LogWarning("Timed out reading '" + path + "'");
							return null;
						}
					}
					if (request.result != UnityWebRequest.Result.Success)
					{
						Debug.LogWarning("Could not read '" + path + "': " + request.error);
						return null;
					}
					return request.downloadHandler.data;
				}
			}
			catch (Exception exception)
			{
				Debug.LogWarning("Could not read '" + path + "': " + exception.Message);
				return null;
			}
		}

		public static IEnumerable<AssemblyReference> LoadStagedReferences()
		{
			List<string> names = new List<string>();
			names.AddRange(FrameworkAssemblyNames);
			names.AddRange(UnityModuleNames);
			names.Add(UnityUIAssemblyName);
			names.Add(HostAssemblyName);

			foreach (string name in names)
			{
				byte[] bytes = ReadStreamingFile(GetStreamingFilePath(ReferenceFolderName, name));
				if (bytes == null)
				{
					continue;
				}
				AssemblyByteReference reference = null;
				try
				{
					reference = new AssemblyByteReference(bytes);
				}
				catch (Exception exception)
				{
					Debug.LogWarning("Bad reference assembly '" + name + "': " + exception.Message);
				}
				if (reference != null)
				{
					yield return reference;
				}
			}
		}
	}
}
