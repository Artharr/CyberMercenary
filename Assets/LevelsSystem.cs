using UnityEngine;

using UnityEngine.UI;

public class LevelsSystem : MonoBehaviour
{
    private GameData gameData;
    private Button Level1Button;
    private Button Level2Button;
    private Button Level3Button;
    private Button Level4Button;
    private Button Level5Button;
    private Button Level6Button;
    private Button Level7Button;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SaveSystem.LoadGame(gameData);

        if (gameData.isLevel7Unlocked)
        {
            Level1Button.interactable = true;
            Level2Button.interactable = true;
            Level3Button.interactable = true;
            Level4Button.interactable = true;
            Level5Button.interactable = true;
            Level6Button.interactable = true;
            Level7Button.interactable = true;
        }
    }

    
}
