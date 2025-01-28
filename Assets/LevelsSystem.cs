using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsSystem : MonoBehaviour
{
    private GameData gameData;
    [SerializeField] private Button[] levelButtons;

    [SerializeField] private GameObject[] scenes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        scenes[0].gameObject.SetActive(true);
        scenes[1].gameObject.SetActive(false);
        gameData = SaveSystem.LoadGame();
    
        ApplyGameDataToLevels();

    }

    private void ApplyGameDataToLevels()
    {
       
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i; 
            bool isUnlocked = gameData.isLevelUnlocked[i]; 

            levelButtons[i].interactable = isUnlocked; 
            levelButtons[i].onClick.RemoveAllListeners(); 
            if (isUnlocked)
            {
                levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex + 1)); 
            }
        }
    }
    public static void LoadLevel(int levelNumber)
    {
        SceneManager.LoadScene($"Level_{levelNumber}");
    }
    
    public static void UnlockLevel(int levelNumber)
    {
        int levelIndex = levelNumber - 1;
        GameData gameData = SaveSystem.LoadGame(); 

        if (levelIndex >= 0 && levelIndex < gameData.isLevelUnlocked.Length)
        {
            gameData.isLevelUnlocked[levelIndex] = true; 
            SaveSystem.SaveGame(gameData);
            Debug.Log($"Poziom {levelIndex + 1} został odblokowany.");
        }
        else
        {
            Debug.LogError($"Nieprawidłowy indeks poziomu: {levelIndex}. Nie można odblokować.");
        }
    }
}
