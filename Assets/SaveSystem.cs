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
            try
            {
                string json = File.ReadAllText(SavePath);
                GameData data = JsonUtility.FromJson<GameData>(json);
            
                if (data == null)
                {
                    Debug.LogWarning("Plik zapisu jest uszkodzony. Tworzenie nowego zapisu.");
                    data = CreateAndSaveNewGame();
                }

                return data;
            }
            catch (Exception e)
            {
                Debug.LogError($"Błąd podczas wczytywania pliku zapisu: {e.Message}. Tworzenie nowego zapisu.");
                return CreateAndSaveNewGame();
            }
        }
        else
        {
            Debug.LogWarning("Brak pliku zapisu. Tworzenie nowego zapisu.");
            return CreateAndSaveNewGame();
        }
    }

    public static GameData CreateAndSaveNewGame()
    {
        GameData newGameData = new GameData(); 
        SaveGame(newGameData); 
        return newGameData;
    }
    public static bool SaveExist()
    {
        return File.Exists(SavePath);
    }
    
}
