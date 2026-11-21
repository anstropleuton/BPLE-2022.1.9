using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class GuidMismatchFinder : EditorWindow
{
    enum IssueType { DuplicateGuid, BrokenReference, OrphanMeta }

    class Issue
    {
        public IssueType type;
        public string path;
        public string detail;
    }

    static readonly string[] YamlExtensions =
    {
        ".unity", ".prefab", ".asset", ".mat", ".controller", ".anim",
        ".overrideController", ".playable", ".mask", ".flare", ".physicMaterial",
        ".physicsMaterial2D", ".guiskin", ".fontsettings", ".lighting",
        ".renderTexture", ".signal", ".mixer", ".spriteatlas", ".brush"
    };

    static readonly Regex GuidRegex = new Regex(@"guid: ([0-9a-fA-F]{32})", RegexOptions.Compiled);

    readonly List<Issue> issues = new List<Issue>();
    Vector2 scroll;
    bool showDuplicates = true, showBroken = true, showOrphans = true;
    bool includePackages = false;
    bool scanned;

    [MenuItem("BPLE/Find GUID Mismatch")]
    static void Open() => GetWindow<GuidMismatchFinder>("GUID Finder");

    void OnGUI()
    {
        includePackages = EditorGUILayout.ToggleLeft("Also scan embedded Packages/ folder", includePackages);

        if (GUILayout.Button("Scan Project", GUILayout.Height(28)))
            Scan();

        if (!scanned) return;

        EditorGUILayout.Space();
        using (new EditorGUILayout.HorizontalScope())
        {
            showDuplicates = GUILayout.Toggle(showDuplicates, $"Duplicates ({Count(IssueType.DuplicateGuid)})", "Button");
            showBroken = GUILayout.Toggle(showBroken, $"Broken refs ({Count(IssueType.BrokenReference)})", "Button");
            showOrphans = GUILayout.Toggle(showOrphans, $"Orphan .meta ({Count(IssueType.OrphanMeta)})", "Button");
        }

        if (issues.Count == 0)
        {
            EditorGUILayout.HelpBox("No GUID problems found.", MessageType.Info);
            return;
        }

        scroll = EditorGUILayout.BeginScrollView(scroll);
        foreach (var issue in issues)
        {
            if (issue.type == IssueType.DuplicateGuid && !showDuplicates) continue;
            if (issue.type == IssueType.BrokenReference && !showBroken) continue;
            if (issue.type == IssueType.OrphanMeta && !showOrphans) continue;

            using (new EditorGUILayout.VerticalScope("box"))
            {
                EditorGUILayout.LabelField(issue.type.ToString(), EditorStyles.boldLabel);
                EditorGUILayout.LabelField(issue.path, EditorStyles.miniLabel);
                EditorGUILayout.LabelField(issue.detail, EditorStyles.wordWrappedMiniLabel);

                var asset = AssetDatabase.LoadAssetAtPath<Object>(issue.path.Replace(".meta", ""));
                using (new EditorGUI.DisabledScope(asset == null))
                {
                    if (GUILayout.Button("Select / Ping", GUILayout.Width(100)))
                    {
                        Selection.activeObject = asset;
                        EditorGUIUtility.PingObject(asset);
                    }
                }
            }
        }
        EditorGUILayout.EndScrollView();
    }

    int Count(IssueType t) => issues.Count(i => i.type == t);

    void Scan()
    {
        issues.Clear();
        var roots = new List<string> { "Assets" };
        if (includePackages) roots.Add("Packages");

        try
        {
            var guidToMetas = new Dictionary<string, List<string>>();
            var metaFiles = roots.SelectMany(r => Directory.GetFiles(r, "*.meta", SearchOption.AllDirectories)).ToList();

            for (int i = 0; i < metaFiles.Count; i++)
            {
                if (i % 200 == 0 &&
                    EditorUtility.DisplayCancelableProgressBar("GUID Finder", "Reading .meta files", (float)i / metaFiles.Count))
                    return;

                string meta = metaFiles[i].Replace('\\', '/');
                string assetPath = meta.Substring(0, meta.Length - 5);

                if (!File.Exists(assetPath) && !Directory.Exists(assetPath))
                    issues.Add(new Issue { type = IssueType.OrphanMeta, path = meta, detail = "No asset exists for this .meta file." });

                var m = GuidRegex.Match(File.ReadAllText(meta));
                if (!m.Success) continue;

                string guid = m.Groups[1].Value.ToLowerInvariant();
                if (!guidToMetas.TryGetValue(guid, out var list))
                    guidToMetas[guid] = list = new List<string>();
                list.Add(meta);
            }

            foreach (var kv in guidToMetas.Where(kv => kv.Value.Count > 1))
                foreach (var path in kv.Value)
                    issues.Add(new Issue
                    {
                        type = IssueType.DuplicateGuid,
                        path = path,
                        detail = $"GUID {kv.Key} is shared by:\n" + string.Join("\n", kv.Value)
                    });

            var yamlFiles = roots
                .SelectMany(r => Directory.GetFiles(r, "*.*", SearchOption.AllDirectories))
                .Where(f => YamlExtensions.Contains(Path.GetExtension(f)))
                .ToList();

            var missingCache = new Dictionary<string, bool>();

            for (int i = 0; i < yamlFiles.Count; i++)
            {
                if (i % 50 == 0 &&
                    EditorUtility.DisplayCancelableProgressBar("GUID Finder", "Checking references", (float)i / yamlFiles.Count))
                    return;

                string file = yamlFiles[i].Replace('\\', '/');
                string text = File.ReadAllText(file);
                if (!text.StartsWith("%YAML")) continue; 

                var seen = new HashSet<string>();
                foreach (Match m in GuidRegex.Matches(text))
                {
                    string guid = m.Groups[1].Value.ToLowerInvariant();
                    if (!seen.Add(guid)) continue;
                    if (guid == "0000000000000000e000000000000000" || guid == "0000000000000000f000000000000000") continue;
                    if (guidToMetas.ContainsKey(guid)) continue;

                    if (!missingCache.TryGetValue(guid, out bool missing))
                    {
                        missing = string.IsNullOrEmpty(AssetDatabase.GUIDToAssetPath(guid));
                        missingCache[guid] = missing;
                    }

                    if (missing)
                        issues.Add(new Issue
                        {
                            type = IssueType.BrokenReference,
                            path = file,
                            detail = $"References missing GUID {guid}"
                        });
                }
            }

            scanned = true;
            Debug.Log($"[GUID Finder] Scan complete: {issues.Count} issue(s) found.");
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }
    }
}
