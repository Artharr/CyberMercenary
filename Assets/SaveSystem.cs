using UnityEngine;
using System.IO;
using System;
public static class SaveSystem
{
    private static string SaveDirectory => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CyberMercenary");
    private static string SavePath => Path.Combine(SaveDirectory, "save.json");

    public static void SaveGame(GameData data)
    {
        if (!Directory.Exists(SaveDirectory))
        {
            Directory.CreateDirectory(SaveDirectory);
        }
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
    }

    public static GameData LoadGame()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            return JsonUtility.FromJson<GameData>(json);
        }
        return null;
    }

    public static bool SaveExist()
    {
        return File.Exists(SavePath);
    }
    
}
