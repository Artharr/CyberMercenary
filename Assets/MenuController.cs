using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
   [Header("Music Settings")]
   [SerializeField] private TextMeshProUGUI musicTextValue = null;
   [SerializeField] private Slider musicSliderValue = null;
   
   [Header("Sound Settings")]
   [SerializeField] private TextMeshProUGUI soundTextValue = null;
   [SerializeField] private Slider soundSliderValue = null;
   
   
   [Header("Default values")]
   [SerializeField] private float defaultMusicVolume = 1.0f;
   [SerializeField] private float defaultSoundVolume = 1.0f;
   [SerializeField] private float defaultBrightness = 1.0f;
   [SerializeField] private bool defaultFullScreen = true;
   [SerializeField] private int defaultQuality = 2;


   [Header("Graphics Settings")] 
   [SerializeField] private Slider brightnessSlider = null;
   [SerializeField] private TextMeshProUGUI brightnessTextValue = null;
   [SerializeField] private Toggle fullScreenToggle = null;
   [SerializeField] private TMP_Dropdown qualityDropdown = null;
   [SerializeField] private Image brightnessOverlay = null;

   private int _qualityLevel;
   private bool _isFullscreen;
   private float _brightnessLevel;

   [Header("Resolution Dropdown")]
   public TMP_Dropdown resolutionDropdown;
   private Resolution[] resolutions;
   
   
   
   [Header("Levels to Load")]
   public string sceneToLoad;
   [SerializeField] private GameObject noSavedGameDialog = null;
   
   private SettingsData settings;

   private void Start()
   {
      
      //Debug.Log($"Loaded music volume: {settings.musicVolume}, sounds volume: {settings.soundsVolume}");
      LoadAudioVolume();

      
      //musicTextValue.text = settings.musicVolume.ToString("0.0");
      //soundTextValue.text = settings.soundsVolume.ToString("0.0");
      LoadGraphicsSettings();
      
      resolutions = Screen.resolutions;
      resolutionDropdown.ClearOptions();
      
      List<string> options = new List<string>();
      int currentResolutionIndex = 0;
      
      for (int i = 0; i < resolutions.Length; i++)
      {
         string option = resolutions[i].width + " x " + resolutions[i].height;
         options.Add(option);
         
         if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) 
         {
            currentResolutionIndex = i;
         }
      }
      resolutionDropdown.AddOptions(options);


      SetResolution(settings.resolutionIndex == -1 ? currentResolutionIndex : settings.resolutionIndex);

      resolutionDropdown.value = settings.resolutionIndex;
      resolutionDropdown.RefreshShownValue();
      SetResolution(settings.resolutionIndex);
      
      musicSliderValue.onValueChanged.AddListener(SetMusicVolume);
      soundSliderValue.onValueChanged.AddListener(SetSoundsVolume);
      brightnessSlider.onValueChanged.AddListener(SetBrightness);
      fullScreenToggle.onValueChanged.AddListener(SetFullscreen);
      resolutionDropdown.onValueChanged.AddListener(SetResolution);
      qualityDropdown.onValueChanged.AddListener(SetQuality);
   }
   
   public void NewGameDialogYes()
   {
      SaveSystem.CreateAndSaveNewGame();
      SceneManager.LoadScene(sceneToLoad);
   }

   public void LoadGameDialogYes()
   {
      if (SaveSystem.SaveExist())
      {
         SceneManager.LoadScene(sceneToLoad);
      }
      else
      {
         noSavedGameDialog.SetActive(true);
      }
   }
   
   public void ExitButton()
   {
      Application.Quit();
      Debug.Log("Game quit");
   }

   public void SetMusicVolume(float volume)
   {
      
      settings.musicVolume = volume;
      if (musicTextValue != null)
      {
         musicTextValue.text = volume.ToString("0.0");
      }
      Debug.Log($"Music volume set to: {volume}");
   }

   public void SetSoundsVolume(float volume)
   {
      settings.soundsVolume = volume;
      if (soundTextValue != null)
      {
         soundTextValue.text = volume.ToString("0.0");
      }
   }

   public void SaveSettings()
   {
      SettingsManager.SaveSettings(settings);
      Debug.Log($"Saving settings: Brightness={settings.brightness}, Quality={settings.quality}, ResolutionIndex={settings.resolutionIndex}, Fullscreen={settings.isFullScreen}");
   }

   public void ResetAudioVolume()
   {
      settings.musicVolume = defaultMusicVolume;
      settings.soundsVolume = defaultSoundVolume;
      musicSliderValue.value = settings.musicVolume;
      soundSliderValue.value = settings.soundsVolume;
      
   }

   public void LoadAudioVolume()
   {
      settings = SettingsManager.LoadSettings();
      musicSliderValue.value = settings.musicVolume;
      soundSliderValue.value = settings.soundsVolume;
      SetMusicVolume(settings.musicVolume);
      SetSoundsVolume(settings.soundsVolume);
   }

   public void LoadGraphicsSettings()
   {
      settings = SettingsManager.LoadSettings();
      brightnessSlider.value = settings.brightness;
      brightnessTextValue.text = settings.brightness.ToString("0.0");
      fullScreenToggle.isOn = settings.isFullScreen;
      qualityDropdown.value = settings.quality;
      
      
      SetQuality(settings.quality);
      SetBrightness(settings.brightness);
      //SetResolution(settings.resolutionIndex);
      SetFullscreen(settings.isFullScreen);
   }

   public void SetBrightness(float brightness)
   {
      settings.brightness = brightness;
      Color overlayColor = brightnessOverlay.color;
      overlayColor.a = 1.0f - brightness; // Im większa jasność, tym mniejsza przezroczystość
      brightnessOverlay.color = overlayColor;
      brightnessTextValue.text = brightness.ToString("0.0");
      Debug.Log($"Brightness set to: {brightness}");
   }

   public void SetFullscreen(bool isFullscreen)
   {
      settings.isFullScreen = isFullscreen;
      Screen.fullScreen = isFullscreen;
   }

   public void SetQuality(int qualityIndex)
   {
      settings.quality = qualityIndex;
   }

   public void SetResolution(int resolutionIndex)
   {
      Resolution resolution = resolutions[resolutionIndex];
      Debug.unityLogger.Log($"Resolution changed to: {resolution.width}x{resolution.height}, Index: {resolutionIndex}");
      settings.resolutionIndex = resolutionIndex;
      Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
   }

   public void ResetGraphicsSettings()
   {
      settings.isFullScreen = defaultFullScreen;
      settings.quality = defaultQuality;
      settings.brightness = defaultBrightness;
      brightnessSlider.value = settings.brightness;
      //brightnessTextValue.text = defaultBrightness.ToString("0.0");
      fullScreenToggle.isOn = settings.isFullScreen;
      qualityDropdown.value = settings.quality;
   }


}
