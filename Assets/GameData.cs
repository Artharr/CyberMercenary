using UnityEngine;
[System.Serializable]
public class GameData
{
    public int experience;
    public bool[] isLevelUnlocked;
    public int damageLevel;
    public int armorLevel;
    public int maxHealthLevel;
    public int healthRegenLevel;
    public int attackSpeedLevel;
    public int expGainLevel;
    public int moveSpeedLevel;
    public int proUpgradeLevel;
    
    
    public GameData()
    {
        experience = 0;

        
        isLevelUnlocked = new bool[7] { true, false, false, false, false, false, false };

        damageLevel = 0;    
        armorLevel = 0;    
        maxHealthLevel = 0;  
        healthRegenLevel = 0; 
        attackSpeedLevel = 0; 
        expGainLevel = 0;  
        moveSpeedLevel = 0; 
        proUpgradeLevel = 0; 
    }
}

