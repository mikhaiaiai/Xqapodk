using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string path = Application.dataPath + "/Configuration/";
    private static string json = ".json";

    public static void Init()
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            
            Save("Localisation", File.ReadAllText(Application.streamingAssetsPath + "/Localisation" + json));
            Save("CraftRecipes", File.ReadAllText(Application.streamingAssetsPath + "/CraftRecipes" + json));
        }
    }

    public static void Save(string filename, string saveString)
    {
        File.WriteAllText(path + filename + json, saveString);
    }
    public static string Load(string filename)
    {
        if (File.Exists(path + filename + json))
        {
            return File.ReadAllText(path + filename + json);
        }
        return null;
    }
}
