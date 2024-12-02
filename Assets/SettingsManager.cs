using System.IO;
using UnityEngine;

public static class SettingsManager
{
    private static readonly string SettingsDirectory = Path.Combine(
        System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), 
        "CyberMercenary"
    );

    private static readonly string SettingsFilePath = Path.Combine(SettingsDirectory, "settings.json");

    public static SettingsData LoadSettings()
    {
        if (File.Exists(SettingsFilePath))
        {
            string json = File.ReadAllText(SettingsFilePath);
            return JsonUtility.FromJson<SettingsData>(json);
        }

        
        return new SettingsData
        {
            musicVolume = 0.5f,
            soundsVolume = 0.5f,
            brightness = 1f,
            isFullScreen = true,
            quality = 2,
            resolutionIndex = -1
            
        };
    }

    public static void SaveSettings(SettingsData settings)
    {
        
        if (!Directory.Exists(SettingsDirectory))
        {
            Directory.CreateDirectory(SettingsDirectory);
        }

        
        string json = JsonUtility.ToJson(settings, true);
        File.WriteAllText(SettingsFilePath, json);
        Debug.Log($"Settings saved to {SettingsFilePath}");
    }
}