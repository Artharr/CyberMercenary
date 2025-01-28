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
    [SerializeField] private LevelUpgradeSystem levelUpgradeSystem;
    private float levelExp;
    private float requiredExp = 100;
    [SerializeField] private Image expBar;

    private GameData gameData;
    private void Start()
    {
        levelManager.onWin += LevelManager_onWin;
        playerData.onDamaged += PlayerData_onDamaged;
    }

    private void Update()
    {
        levelExp = GlobalData.tempEXP;
        if (levelExp >= requiredExp)
        {
            levelUpgradeSystem.Open();
            levelExp = 0;
            GlobalData.tempEXP = 0;
        }
        expBar.fillAmount = Mathf.Lerp(expBar.fillAmount, levelExp / requiredExp,.1f);
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
