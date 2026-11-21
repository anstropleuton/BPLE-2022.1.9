using UnityEngine;

namespace Innovation.Script
{
	public static class HybridCLRBootstrap
	{
		public static int LoadedAssemblyCount { get; private set; }

#if ENABLE_IL2CPP && !UNITY_EDITOR
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Initialize()
		{
			int loaded = 0;
			foreach (string name in ScriptReferenceSet.MetadataAssemblyNames)
			{
				string path = ScriptReferenceSet.GetStreamingFilePath(ScriptReferenceSet.MetadataFolderName, name);
				byte[] bytes = ScriptReferenceSet.ReadStreamingFile(path);
				if (bytes == null)
				{
					Debug.LogWarning("[HybridCLRBootstrap] No supplementary metadata for '" + name + "' (" + path + ")");
					continue;
				}

				HybridCLR.LoadImageErrorCode error = HybridCLR.RuntimeApi.LoadMetadataForAOTAssembly(bytes, HybridCLR.HomologousImageMode.SuperSet);
				if (error == HybridCLR.LoadImageErrorCode.OK || error == HybridCLR.LoadImageErrorCode.HOMOLOGOUS_ASSEMBLY_HAS_LOADED)
				{
					loaded++;
				}
				else
				{
					Debug.LogError("[HybridCLRBootstrap] LoadMetadataForAOTAssembly('" + name + "') failed: " + error);
				}
			}
			LoadedAssemblyCount = loaded;
			Debug.Log("[HybridCLRBootstrap] Loaded supplementary metadata for " + loaded + " of " + ScriptReferenceSet.MetadataAssemblyNames.Length + " assemblies.");
		}
#endif
	}
}
