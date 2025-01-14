using TMPro;
using UnityEngine;
using UnityEngine.UI;
//dodac logike przyciskow klikania
public class UpgradeSystem : MonoBehaviour
{
    private GameData gameData;
    [SerializeField] private GameObject[] progressBarContainers;
    private Image[][] progressBars;

    [SerializeField] private TMP_Text experienceValue;
    private int maxLevel = 8; 
    
    [SerializeField] private TMP_Text[] upgradeCostTexts;
    private int[] upgradeCosts = { 100, 200, 300, 400, 500, 600, 700, 800, -1 }; // -1 jest w celu by nie wykraczac poza zakres tablicy, zanim wyswietli sie max po ulepszeniu do ostatecznego poziomu

    
    [SerializeField] private Button[] upgradeButtons;
    
    [SerializeField] private TMP_Text proUpgradeText;
    private void Start()
    {
        UpdateExperience();
        InitializeProgressBars();
        UpdateAllProgressBarsAndCosts();
        UpdateUpgradeButtons();
        
        upgradeButtons[0].onClick.AddListener(UpgradeDamage);
        upgradeButtons[1].onClick.AddListener(UpgradeArmor);
        upgradeButtons[2].onClick.AddListener(UpgradeMaxHealth);
        upgradeButtons[3].onClick.AddListener(UpgradeHealthRegen);
        upgradeButtons[4].onClick.AddListener(UpgradeAttackSpeed);
        upgradeButtons[5].onClick.AddListener(UpgradeExpGain);
        upgradeButtons[6].onClick.AddListener(UpgradeMoveSpeed);
        upgradeButtons[7].onClick.AddListener(UpgradeProUpgrade); // Pro Upgrade
    }

    private void InitializeProgressBars()
    {
        progressBars = new Image[progressBarContainers.Length][];
        
        for (int i = 0; i < progressBarContainers.Length; i++)
        {
            progressBars[i] = new Image[maxLevel];
            for (int j = 0; j < maxLevel; j++)
            {
                
                progressBars[i][j] = progressBarContainers[i].transform.GetChild(j).GetComponent<Image>();
            }
        }
    }
    
    
    public void UpdateProgressBar(int statIndex, int level)
    {
        for (int i = 0; i < maxLevel; i++)
        {
            progressBars[statIndex][i].color = (i < level) ? Color.red : Color.white;
        }
    }

    private void UpdateExperience()
    {
        gameData = SaveSystem.LoadGame();
        experienceValue.text = gameData.experience.ToString();
        SaveSystem.SaveGame(gameData);
    }
    
    private void UpdateUpgradeButtons()
    {
        gameData = SaveSystem.LoadGame();
        if (gameData != null)
        {
            // Sprawdź dla każdego przycisku
            UpdateButtonState(upgradeButtons[0], upgradeCostTexts[0], gameData.damageLevel);
            UpdateButtonState(upgradeButtons[1], upgradeCostTexts[1], gameData.armorLevel);
            UpdateButtonState(upgradeButtons[2], upgradeCostTexts[2], gameData.maxHealthLevel);
            UpdateButtonState(upgradeButtons[3], upgradeCostTexts[3], gameData.healthRegenLevel);
            UpdateButtonState(upgradeButtons[4], upgradeCostTexts[4], gameData.attackSpeedLevel);
            UpdateButtonState(upgradeButtons[5], upgradeCostTexts[5], gameData.expGainLevel);
            UpdateButtonState(upgradeButtons[6], upgradeCostTexts[6], gameData.moveSpeedLevel);
            
            proUpgradeText.text = $"Level: {gameData.proUpgradeLevel}";
            if (AreAllUpgradesMaxed())
            {
                int cost = 1000 + (gameData.proUpgradeLevel * 500);
                upgradeButtons[7].interactable = gameData.experience >= cost; 
                upgradeCostTexts[7].text =  cost.ToString();
            }
            else
            {
                
                upgradeButtons[7].interactable = false; 
                upgradeCostTexts[7].text = "----";
            }
        }
    }
    private void UpdateButtonState(Button button, TMP_Text costText, int currentLevel)
    {
        
        if (currentLevel >= maxLevel)
        {
            button.interactable = false; 
            costText.text = "Max"; 
        }
        else
        {
            int cost = GetUpgradeCostInt(currentLevel);
            if (gameData.experience < cost)
            {
                button.interactable = false; 
                costText.text =  cost.ToString();
            }
            else
            {
                button.interactable = true; 
                costText.text = cost.ToString();
            }
        }
    }

    private void UpdateAllProgressBarsAndCosts()
    {
        

        if (gameData != null)
        {
            UpdateProgressBar(0, gameData.damageLevel);
            upgradeCostTexts[0].text = GetUpgradeCostString(gameData.damageLevel);
            UpdateProgressBar(1, gameData.armorLevel);
            upgradeCostTexts[1].text = GetUpgradeCostString(gameData.armorLevel);
            UpdateProgressBar(2, gameData.maxHealthLevel);
            upgradeCostTexts[2].text = GetUpgradeCostString(gameData.moveSpeedLevel);
            UpdateProgressBar(3, gameData.healthRegenLevel);
            upgradeCostTexts[3].text = GetUpgradeCostString(gameData.healthRegenLevel);
            UpdateProgressBar(4, gameData.attackSpeedLevel);
            upgradeCostTexts[4].text = GetUpgradeCostString(gameData.attackSpeedLevel);
            UpdateProgressBar(5, gameData.expGainLevel);
            upgradeCostTexts[5].text = GetUpgradeCostString(gameData.expGainLevel);
            UpdateProgressBar(6, gameData.moveSpeedLevel);
            upgradeCostTexts[6].text = GetUpgradeCostString(gameData.moveSpeedLevel);
            
            
            /*
            if (AreAllUpgradesMaxed())
            {
                int cost = 1000 + (gameData.proUpgradeLevel * 500);
                upgradeButtons[7].interactable = gameData.experience >= cost; 
                upgradeCostTexts[7].text = "Cost: " + cost;
            }
            else
            {
                upgradeButtons[7].interactable = false;
                upgradeCostTexts[7].text = "Unlock all upgrades first!";
            }
            */
        }
    }

    private bool AreAllUpgradesMaxed()
    {
        return gameData.damageLevel >= maxLevel &&
               gameData.armorLevel >= maxLevel &&
               gameData.maxHealthLevel >= maxLevel &&
               gameData.healthRegenLevel >= maxLevel &&
               gameData.attackSpeedLevel >= maxLevel &&
               gameData.expGainLevel >= maxLevel &&
               gameData.moveSpeedLevel >= maxLevel;
    }

    private string GetUpgradeCostString(int currentLevel)
    {
        return upgradeCosts[currentLevel].ToString();
    }
    
    private int GetUpgradeCostInt(int currentLevel)
    {
        return upgradeCosts[currentLevel];
    }

    public void UpgradeDamage()
    {
        Debug.Log($"Damage level: " + gameData.damageLevel);
        int cost = GetUpgradeCostInt(gameData.damageLevel);
        if (gameData.damageLevel < maxLevel && gameData.experience >= cost)
        {
            gameData.experience -= cost;
            gameData.damageLevel++;
            SaveSystem.SaveGame(gameData);
            UpdateExperience();
            UpdateAllProgressBarsAndCosts();
            UpdateUpgradeButtons();
        }
    }

    public void UpgradeArmor()
    {
        int cost = GetUpgradeCostInt(gameData.armorLevel);
        if (gameData.armorLevel < maxLevel && gameData.experience >= cost)
        {
            gameData.experience -= cost;
            gameData.armorLevel++;
            SaveSystem.SaveGame(gameData);
            UpdateExperience();
            UpdateAllProgressBarsAndCosts();
            UpdateUpgradeButtons();
        }
    }

    public void UpgradeMaxHealth()
    {
        int cost = GetUpgradeCostInt(gameData.maxHealthLevel);
        if (gameData.maxHealthLevel < maxLevel && gameData.experience >= cost)
        {
            gameData.experience -= cost;
            gameData.maxHealthLevel++;
            SaveSystem.SaveGame(gameData);
            UpdateExperience();
            UpdateAllProgressBarsAndCosts();
            UpdateUpgradeButtons();
        }
    }

    public void UpgradeHealthRegen()
    {
        
        int cost = GetUpgradeCostInt(gameData.healthRegenLevel);
        if (gameData.healthRegenLevel < maxLevel && gameData.experience >= cost)
        {
            gameData.experience -= cost;
            gameData.healthRegenLevel++;
            SaveSystem.SaveGame(gameData);
            UpdateExperience();
            UpdateAllProgressBarsAndCosts();
            UpdateUpgradeButtons();
        }
    }

    public void UpgradeAttackSpeed()
    {
        int cost = GetUpgradeCostInt(gameData.attackSpeedLevel);
        if (gameData.attackSpeedLevel < maxLevel && gameData.experience >= cost)
        {
            gameData.experience -= cost;
            gameData.attackSpeedLevel++;
            SaveSystem.SaveGame(gameData);
            UpdateExperience();
            UpdateAllProgressBarsAndCosts();
            UpdateUpgradeButtons();
        }
    }

    public void UpgradeExpGain()
    {
        int cost = GetUpgradeCostInt(gameData.expGainLevel);
        if (gameData.expGainLevel < maxLevel && gameData.experience >= cost)
        {
            gameData.experience -= cost;
            gameData.expGainLevel++;
            SaveSystem.SaveGame(gameData);
            UpdateExperience();
            UpdateAllProgressBarsAndCosts();
            UpdateUpgradeButtons();
        }
    }

    public void UpgradeMoveSpeed()
    {
        int cost = GetUpgradeCostInt(gameData.moveSpeedLevel);
        if (gameData.moveSpeedLevel < maxLevel && gameData.experience >= cost)
        {
            gameData.experience -= cost;
            gameData.moveSpeedLevel++;
            SaveSystem.SaveGame(gameData);
            UpdateExperience();
            UpdateAllProgressBarsAndCosts();
            UpdateUpgradeButtons();
        }
    }

    public void UpgradeProUpgrade()
    {
        int cost = 1000 + (gameData.proUpgradeLevel * 500); 

        if (AreAllUpgradesMaxed() && gameData.experience >= cost)
        {
            gameData.experience -= cost;
            gameData.proUpgradeLevel++; 
            SaveSystem.SaveGame(gameData);
            UpdateExperience();
            UpdateUpgradeButtons(); 
        }
    }
    
   
}
