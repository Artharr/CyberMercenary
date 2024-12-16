using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    private GameData gameData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void UpradeDamage()
    {
        if (gameData.damageLevel < 8)
        {
            gameData.damageLevel++;
            SaveSystem.SaveGame(gameData);
        }
    }

    public void UpgradeArmor()
    {
        if (gameData.armorLevel < 8)
        {
            gameData.armorLevel++;
            SaveSystem.SaveGame(gameData);
        }
    }

    public void UpgradeMaxSpeed()
    {
        if (gameData.maxHealthLevel<8)
        {
         gameData.maxHealthLevel++;
         SaveSystem.SaveGame(gameData);
        }
    }

    public void UpgradeHealthRegen()
    {
        if (gameData.healthRegenLevel < 8)
        {
            gameData.healthRegenLevel++;
            SaveSystem.SaveGame(gameData);
        }
    }

    public void UpgradeAttackSpeed()
    {
        if (gameData.attackSpeedLevel<8)
        {
            gameData.attackSpeedLevel++;
            SaveSystem.SaveGame(gameData);
        }
    }

    public void UpgradeExpGain()
    {
        if (gameData.expGainLevel < 8)
        {
            gameData.expGainLevel++;
            SaveSystem.SaveGame(gameData);
        }
    }

    public void UpgradeMoveSpeed()
    {
        if (gameData.moveSpeedLevel < 8)
        {
            gameData.moveSpeedLevel++;
            SaveSystem.SaveGame(gameData);
        }
    }

    public void UpgradeProUpgrade()
    {
        if (gameData.proUpgradeLevel < 8)
        {
            gameData.proUpgradeLevel++;
            SaveSystem.SaveGame(gameData);
        }
    }
    
   
}
