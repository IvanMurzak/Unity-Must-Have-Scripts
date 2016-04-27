using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SceneEnumGenerator : UnityEditor.AssetModificationProcessor
{
    private const string SCENES_ENUM_NAME = "BuildingScenes";
    private static string[] TRIGGER_FILE_ENDS = new string[] { ".unity", "EditorBuildSettings.asset" };

    private static string Path()
    {
        return "Assets/Game Package/scripts/utils/enum/" + SCENES_ENUM_NAME + ".cs";
    }

    private static string ConvertToEnumName(string name)
    {
        return name
            .Replace(" ", "_")
            .Replace("@", "_")
            .Replace(":", "_")
            .Replace(";", "_")
            .Replace("=", "_")
            .Replace("-", "_")
            .Replace("!", "_")
            .Replace("#", "_")
            .Replace("$", "_")
            .Replace("%", "_")
            .Replace("^", "_")
            .Replace("*", "_")
            .Replace("(", "_")
            .Replace(")", "_")
            .Replace(".", "_")
            .Replace(",", "_")
            .Replace("~", "_");
    }

    private static string[] SceneNames()
    {
        List<string> temp = new List<string>();
        foreach (UnityEditor.EditorBuildSettingsScene scene in UnityEditor.EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                string name = scene.path.Substring(scene.path.LastIndexOf('/') + 1);
                name = name.Substring(0, name.Length - 6);
                temp.Add(name);
            }
        }
        return temp.ToArray();
    }

    private static bool IsTriggers(string str)
    {
        foreach (string end in TRIGGER_FILE_ENDS)
            if (str.EndsWith(end)) return true;
        return false;
    }

    public static void OnWillCreateAsset(string path)
    {
        // Debug.Log("OnWillCreateAsset:" + path);
        if (IsTriggers(path))
            Generate();
    }

    public static AssetDeleteResult OnWillDeleteAsset(string path, RemoveAssetOptions options)
    {
        // Debug.Log("OnWillDeleteAsset:" + path);
        if (IsTriggers(path))
            Generate();

        return AssetDeleteResult.DidDelete;
    }

    public static AssetMoveResult OnWillMoveAsset(string pathOld, string pathNew)
    {
        // Debug.Log("OnWillMoveAsset:" + pathOld);
        if (IsTriggers(pathNew))
            Generate();
        
        return AssetMoveResult.DidMove;
    }

    public static string[] OnWillSaveAssets(string[] paths)
    {
        bool needToGenerate = false;
        foreach (string path in paths)
        {
            // Debug.Log("OnWillSaveAssets:" + path);
            if (IsTriggers(path))
            {
                needToGenerate = true;
                break;
            }
        }

        if (needToGenerate) Generate();
        return paths;
    }

    public static void Generate()
    {
        string path = Path();
        Debug.Log("Generating Scene's enum: " + path);
        using (StreamWriter outfile = new StreamWriter(path))
        {
            outfile.WriteLine("using System.ComponentModel;");
            outfile.WriteLine("");
            outfile.WriteLine("// Auto generated code");
            outfile.WriteLine("");
            outfile.WriteLine("public enum " + SCENES_ENUM_NAME);
            outfile.WriteLine("{");

            string[] sceneNames = SceneNames();
            foreach (string sceneName in sceneNames)
            {
                outfile.WriteLine("[Description(\"" + sceneName + "\")]");
                outfile.WriteLine(ConvertToEnumName(sceneName) + ",");
                outfile.WriteLine("");
            }

            outfile.WriteLine("}");
        }
        AssetDatabase.Refresh();
    }
}
