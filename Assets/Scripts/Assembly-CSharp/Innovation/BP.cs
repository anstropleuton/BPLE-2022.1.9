using System.Collections.Generic;
using UnityEngine;

namespace Innovation
{
	public static class BP
	{
		public static IAddonService AddonService { get; set; }

		public static ICoreService CoreService { get; set; }

		public static IPartService PartService { get; set; }

		public static string GetAddonDataPath()
		{
			return AddonService.GetAddonDataPath();
		}

		public static string LoadFileAsString(string path)
		{
			return AddonService.LoadFileAsString(path);
		}

		public static byte[] LoadFileAsBytes(string path)
		{
			return AddonService.LoadFileAsBytes(path);
		}

		public static void RunScript(string code, bool printCode = true)
		{
			AddonService.RunScript(code, printCode);
		}

		public static void RunScriptFromFile(string path, bool printCode = true)
		{
			AddonService.RunScriptFromFile(path, printCode);
		}

		public static Texture2D LoadTexture(byte[] data, string name = null)
		{
			return AddonService.LoadTexture(data, name);
		}

		public static Texture2D LoadTextureFromFile(string path, string name = null)
		{
			return AddonService.LoadTextureFromFile(path, name);
		}

		public static AudioClip LoadAudio(byte[] data, string name = null)
		{
			return AddonService.LoadAudio(data, name);
		}

		public static AudioClip LoadAudioFromFile(string path, string name = null)
		{
			return AddonService.LoadAudioFromFile(path, name);
		}

		public static IContraptionData LoadContraptionData(string text)
		{
			return AddonService.LoadContraptionData(text);
		}

		public static IContraptionData LoadContraptionDataFromFile(string path)
		{
			return AddonService.LoadContraptionDataFromFile(path);
		}

		public static void SetTexturePack(Texture2D texture)
		{
			AddonService.SetTexturePack(texture);
		}

		public static void SetAudioPack(AudioClip audioClip, string name = null)
		{
			AddonService.SetAudioPack(audioClip, name);
		}

		public static AddonPackage FindPackage(string id)
		{
			return AddonService.FindPackage(id);
		}

		public static AddonPackage FindCurrentPackage()
		{
			return AddonService.FindCurrentPackage();
		}

		public static AddonComponent FindAddonComponent(string name)
		{
			return AddonService.FindAddonComponent(name);
		}

		public static IReadOnlyList<AddonComponent> FindAddonComponents(string name)
		{
			return AddonService.FindAddonComponents(name);
		}

		public static AddonBackground CreateBackground(Texture2D texture = null, Color? color = null, LocationMode locationMode = LocationMode.CameraAndScreen)
		{
			return AddonService.CreateBackground(texture, color, locationMode);
		}

		public static AddonVideoPlayer CreateVideoPlayer(string path, LocationMode locationMode = LocationMode.CameraAndScreen)
		{
			return AddonService.CreateVideoPlayer(path, locationMode);
		}

		public static GameObject CreateGameObject(GameObjectTemplate template, IResourceResolver resolver = null)
		{
			return AddonService.CreateGameObject(template, resolver);
		}

		public static IBasePart CreateCustomPart(CustomPartTemplate template, IResourceResolver resolver = null)
		{
			return AddonService.CreateCustomPart(template, resolver);
		}

		public static string 获取插件系统路径()
		{
			return GetAddonDataPath();
		}

		public static string 加载文件为字符串(string path)
		{
			return LoadFileAsString(path);
		}

		public static byte[] 加载文件为字节数组(string path)
		{
			return LoadFileAsBytes(path);
		}

		public static void 执行脚本(string code, bool printCode = true)
		{
			RunScript(code, printCode);
		}

		public static void 执行脚本文件(string path, bool printCode = true)
		{
			RunScriptFromFile(path, printCode);
		}

		public static Texture2D 加载贴图(byte[] data, string name = null)
		{
			return LoadTexture(data, name);
		}

		public static Texture2D 加载贴图文件(string path, string name = null)
		{
			return LoadTextureFromFile(path, name);
		}

		public static AudioClip 加载音频(byte[] data, string name = null)
		{
			return LoadAudio(data, name);
		}

		public static AudioClip 加载音频文件(string path, string name = null)
		{
			return LoadAudioFromFile(path, name);
		}

		public static IContraptionData 加载载具存档(string text)
		{
			return LoadContraptionData(text);
		}

		public static IContraptionData 加载载具存档文件(string path)
		{
			return LoadContraptionDataFromFile(path);
		}

		public static void 设置材质包(Texture2D texture)
		{
			SetTexturePack(texture);
		}

		public static void 设置音频包(AudioClip audioClip, string name = null)
		{
			SetAudioPack(audioClip, name);
		}

		public static AddonPackage 获取插件包(string id)
		{
			return FindPackage(id);
		}

		public static AddonPackage 获取当前插件包()
		{
			return FindCurrentPackage();
		}

		public static AddonComponent 获取插件组件(string name)
		{
			return FindAddonComponent(name);
		}

		public static IReadOnlyList<AddonComponent> 获取插件组件列表(string name)
		{
			return FindAddonComponents(name);
		}

		public static AddonBackground 创建自定义背景(Texture2D texture = null, Color? color = null, LocationMode locationMode = LocationMode.CameraAndScreen)
		{
			return CreateBackground(texture, color, locationMode);
		}

		public static AddonVideoPlayer 创建视频播放器(string path, LocationMode locationMode = LocationMode.CameraAndScreen)
		{
			return CreateVideoPlayer(path, locationMode);
		}

		public static GameObject 创建游戏物体(GameObjectTemplate template, IResourceResolver resolver = null)
		{
			return CreateGameObject(template, resolver);
		}

		public static IBasePart 创建自定义部件(CustomPartTemplate template, IResourceResolver resolver = null)
		{
			return CreateCustomPart(template, resolver);
		}

		public static void Write(object arg)
		{
			CoreService.Write(arg);
		}

		public static void Write(params object[] args)
		{
			CoreService.Write(args);
		}

		public static void WriteLine(object arg)
		{
			CoreService.WriteLine(arg);
		}

		public static void WriteLine(params object[] args)
		{
			CoreService.WriteLine(args);
		}

		public static void Clear()
		{
			CoreService.Clear();
		}

		public static void EnableFeedback()
		{
			CoreService.EnableFeedback();
		}

		public static void DisableFeedback()
		{
			CoreService.DisableFeedback();
		}

		public static void Feedback(object arg)
		{
			CoreService.Feedback(arg);
		}

		public static void Feedback(params object[] args)
		{
			CoreService.Feedback(args);
		}

		public static object GetSettingsValue(string name)
		{
			return CoreService.GetSettingsValue(name);
		}

		public static void SetSettingsValue(string name, object value)
		{
			CoreService.SetSettingsValue(name, value);
		}

		public static void ResetSettings()
		{
			CoreService.ResetSettings();
		}

		public static object GetUserSettingsValue(string name)
		{
			return CoreService.GetUserSettingsValue(name);
		}

		public static void SetUserSettingsValue(string name, object value)
		{
			CoreService.SetUserSettingsValue(name, value);
		}

		public static void ResetUserSettings()
		{
			CoreService.ResetUserSettings();
		}

		public static void SaveUserSettings()
		{
			CoreService.ResetUserSettings();
		}

		public static GameState GetGameState()
		{
			return CoreService.GetGameState();
		}

		public static void 输出(object arg)
		{
			CoreService.Write(arg);
		}

		public static void 输出(params object[] args)
		{
			CoreService.Write(args);
		}

		public static void 输出行(object arg)
		{
			WriteLine(arg);
		}

		public static void 输出行(params object[] args)
		{
			WriteLine(args);
		}

		public static void 清除()
		{
			Clear();
		}

		public static void 启用输出反馈()
		{
			EnableFeedback();
		}

		public static void 关闭输出反馈()
		{
			DisableFeedback();
		}

		public static void 反馈(object arg)
		{
			Feedback(arg);
		}

		public static void 反馈(params object[] args)
		{
			Feedback(args);
		}

		public static void 获取选项值(string name)
		{
			GetSettingsValue(name);
		}

		public static void 设置选项值(string name, object value)
		{
			SetSettingsValue(name, value);
		}

		public static void 重置设置()
		{
			ResetSettings();
		}

		public static void 获取用户选项值(string name)
		{
			GetUserSettingsValue(name);
		}

		public static void 设置用户选项值(string name, object value)
		{
			SetUserSettingsValue(name, value);
		}

		public static void 重置用户设置()
		{
			ResetUserSettings();
		}

		public static void 保存用户设置()
		{
			SaveUserSettings();
		}

		public static GameState 获取游戏状态()
		{
			return GetGameState();
		}

		public static IReadOnlyList<IBasePart> GetAllParts()
		{
			return PartService.GetAllParts();
		}

		public static IReadOnlyList<IBasePart> GetAllRuntimeParts()
		{
			return PartService.GetAllRuntimeParts();
		}

		public static IBasePart SelectPart(int x, int y, PartTypeWrapper partType = default(PartTypeWrapper), int partIndex = -1)
		{
			return PartService.SelectPart(x, y, partType.HasValue ? partType.Value : PartTypeCode.All, partIndex);
		}

		public static IReadOnlyList<IBasePart> SelectParts(int x, int y, int width, int height, PartTypeWrapper partType = default(PartTypeWrapper), int partIndex = -1)
		{
			return PartService.SelectParts(x, y, width, height, partType.HasValue ? partType.Value : PartTypeCode.All, partIndex);
		}

		public static IReadOnlyList<IBasePart> InvertSelection(IReadOnlyList<IBasePart> parts)
		{
			return PartService.InvertSelection(parts);
		}

		public static IBasePart SetPart(int x, int y, PartTypeWrapper partType, int partIndex)
		{
			return PartService.SetPart(x, y, partType.Value, partIndex);
		}

		public static IReadOnlyList<IBasePart> SetParts(int x, int y, int width, int height, PartTypeWrapper partType, int partIndex)
		{
			return PartService.SetParts(x, y, width, height, partType.Value, partIndex);
		}

		public static IReadOnlyList<IBasePart> SetPartsInterval(int x, int y, int width, int height, int deltaX, int deltaY, PartTypeWrapper partType, int partIndex)
		{
			return PartService.SetPartsInterval(x, y, width, height, deltaX, deltaY, partType.Value, partIndex);
		}

		public static void MoveParts(IReadOnlyList<IBasePart> parts, int x, int y)
		{
			PartService.MoveParts(parts, x, y);
		}

		public static void RotateParts(IReadOnlyList<IBasePart> parts, int times)
		{
			PartService.RotateParts(parts, times);
		}

		public static IReadOnlyList<IBasePart> CopyParts(IReadOnlyList<IBasePart> parts, int x, int y)
		{
			return PartService.CopyParts(parts, x, y);
		}

		public static IReadOnlyList<IBasePart> ReplaceParts(IReadOnlyList<IBasePart> parts, PartTypeWrapper partType, int partIndex)
		{
			return PartService.ReplaceParts(parts, partType.Value, partIndex);
		}

		public static void RemoveParts(IReadOnlyList<IBasePart> parts)
		{
			PartService.RemoveParts(parts);
		}

		public static IBasePart SetRuntimePart(int x, int y, int rotation, bool flipped, PartTypeWrapper partType, int partIndex)
		{
			return PartService.SetRuntimePart(x, y, rotation, flipped, partType.Value, partIndex);
		}

		public static string GetContraptionName()
		{
			return PartService.GetContraptionName();
		}

		public static void SaveContraption()
		{
			PartService.SaveContraption();
		}

		public static void MoveContraption(int x, int y)
		{
			PartService.MoveContraption(x, y);
		}

		public static IContraptionData CopyContraption()
		{
			return PartService.CopyContraption();
		}

		public static void PasteContraption(IContraptionData data, int x = 0, int y = 0, bool absolute = false)
		{
			PartService.PasteContraption(data, x, y, absolute);
		}

		public static IReadOnlyList<IBasePart> 获取建造时部件()
		{
			return GetAllParts();
		}

		public static IReadOnlyList<IBasePart> 获取运行时部件()
		{
			return GetAllRuntimeParts();
		}

		public static IBasePart 选择单部件(int x, int y, PartTypeWrapper partType = default(PartTypeWrapper), int partIndex = -1)
		{
			return SelectPart(x, y, partType, partIndex);
		}

		public static IReadOnlyList<IBasePart> 选择部件(int x, int y, int width, int height, PartTypeWrapper partType = default(PartTypeWrapper), int partIndex = -1)
		{
			return SelectParts(x, y, width, height, partType, partIndex);
		}

		public static IReadOnlyList<IBasePart> 反选部件(IReadOnlyList<IBasePart> parts)
		{
			return InvertSelection(parts);
		}

		public static void 放置单部件(int x, int y, PartTypeWrapper partType, int partIndex)
		{
			SetPart(x, y, partType, partIndex);
		}

		public static void 放置部件(int x, int y, int width, int height, PartTypeWrapper partType, int partIndex)
		{
			SetParts(x, y, width, height, partType, partIndex);
		}

		public static void 放置间隔部件(int x, int y, int width, int height, int deltaX, int deltaY, PartTypeWrapper partType, int partIndex)
		{
			SetPartsInterval(x, y, width, height, deltaX, deltaY, partType, partIndex);
		}

		public static void 移动部件(IReadOnlyList<IBasePart> parts, int x, int y)
		{
			MoveParts(parts, x, y);
		}

		public static void 转动部件(IReadOnlyList<IBasePart> parts, int times)
		{
			RotateParts(parts, times);
		}

		public static IReadOnlyList<IBasePart> 复制部件(IReadOnlyList<IBasePart> parts, int x, int y)
		{
			return CopyParts(parts, x, y);
		}

		public static IReadOnlyList<IBasePart> 替换部件(IReadOnlyList<IBasePart> parts, PartTypeWrapper partType, int partIndex)
		{
			return ReplaceParts(parts, partType, partIndex);
		}

		public static void 移除部件(IReadOnlyList<IBasePart> parts)
		{
			RemoveParts(parts);
		}

		public static IBasePart 放置运行时部件(int x, int y, int rotation, bool flipped, PartTypeWrapper partType, int partIndex)
		{
			return SetRuntimePart(x, y, rotation, flipped, partType.Value, partIndex);
		}

		public static string 获取载具存档名()
		{
			return GetContraptionName();
		}

		public static void 保存载具()
		{
			SaveContraption();
		}

		public static void 移动载具(int x, int y)
		{
			MoveContraption(x, y);
		}

		public static IContraptionData 复制载具()
		{
			return CopyContraption();
		}

		public static void 粘贴载具(IContraptionData data, int x = 0, int y = 0, bool absolute = false)
		{
			PasteContraption(data, x, y, absolute);
		}
	}
}
