using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsSystem : MonoBehaviour
{
    private GameData gameData;
    [SerializeField]private Button Level1Button;
    [SerializeField] private Button Level2Button;
    [SerializeField] private Button Level3Button;
    [SerializeField] private Button Level4Button;
    [SerializeField] private Button Level5Button;
    [SerializeField] private Button Level6Button;
    [SerializeField] private Button Level7Button;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //SaveSystem.LoadGame(gameData);

        //if (gameData.isLevel7Unlocked)
        //{
        //    Level1Button.interactable = true;
        //    Level2Button.interactable = true;
        //    Level3Button.interactable = true;
        //    Level4Button.interactable = true;
        //    Level5Button.interactable = true;
        //    Level6Button.interactable = true;
        //    Level7Button.interactable = true;
        //}
        Level1Button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Level_1");
        });
    }

    
}
