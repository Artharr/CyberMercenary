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

    public static void LoadGame(GameData loadedData)
    {
        string json = File.ReadAllText(SavePath);
        GameData data = JsonUtility.FromJson<GameData>(json);
        
        data.experience = loadedData.experience;
        data.isLevel1Unlocked = loadedData.isLevel1Unlocked;
        data.isLevel2Unlocked = loadedData.isLevel2Unlocked;
        data.isLevel3Unlocked = loadedData.isLevel3Unlocked;
        data.isLevel4Unlocked = loadedData.isLevel4Unlocked;
        data.isLevel5Unlocked = loadedData.isLevel5Unlocked;
        data.isLevel6Unlocked = loadedData.isLevel6Unlocked;
        data.isLevel7Unlocked = loadedData.isLevel7Unlocked;
        data.damageLevel = loadedData.damageLevel;
        data.armorLevel = loadedData.armorLevel;
        data.maxHealthLevel = loadedData.maxHealthLevel;
        data.healthRegenLevel = loadedData.healthRegenLevel;
        data.attackSpeedLevel = loadedData.attackSpeedLevel;
        
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
