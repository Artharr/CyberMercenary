using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Image hpImageBar;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private TextMeshProUGUI deathPointText;
    [SerializeField] private TextMeshProUGUI winPointText;
    [SerializeField]private LevelManager levelManager;
    private GameData gameData;
    private void Start()
    {
        levelManager.onWin += LevelManager_onWin;
        
        playerData.onDamaged += PlayerData_onDamaged;
    }

    private void LevelManager_onWin(object sender, int e)
    {
        winScreen.SetActive(true);

        winPointText.text = "EXP Collected: "+e.ToString();
    }

    private void PlayerData_onDamaged(object sender, float e)
    {

        hpImageBar.fillAmount = e;
        if (e < 0) {
           
            deathPointText.text = "EXP Collected: "+GlobalData.reward.ToString();
            deathScreen.SetActive(true);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("InGameMenu");
    }
}
