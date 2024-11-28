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
   
   [SerializeField] private float defaultMusicVolume = 1.0f;
   [SerializeField] private float defaultSoundVolume = 1.0f;
   
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
      
      

      
      musicSliderValue.onValueChanged.AddListener(SetMusicVolume);
      soundSliderValue.onValueChanged.AddListener(SetSoundsVolume);
   }
   
   public void NewGameDialogYes()
   {
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
}
