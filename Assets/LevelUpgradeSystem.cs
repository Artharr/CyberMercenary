using TMPro;
using UnityEngine;

public class LevelUpgradeSystem : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private TextMeshProUGUI choice1;
    [SerializeField] private TextMeshProUGUI choice2;
    [SerializeField] private TextMeshProUGUI choice3;
    int num1, num2, num3;
    int upgradeCount = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            RollWeapons();
        }
    }

    public void RollWeapons()
    {
        num1 = Random.Range(0, playerData.weapons.Length);
        num2 = Random.Range(0, playerData.weapons.Length);
        num3 = Random.Range(0, playerData.weapons.Length);

        choice1.text = playerData.weapons[num1].Name;
        choice2.text = playerData.weapons[num2].Name;
        choice3.text = playerData.weapons[num3].Name;
    }

    public void Open()
    {
        if(upgradeCount >= 20) { return; }
        RollWeapons();
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Close()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void UpgradeSelected(int ID)
    {
        switch (ID)
        {
            case 0:
                Upgrade(playerData.weapons[num1]);
                break;
            case 1:
                Upgrade(playerData.weapons[num2]);
                break;
            case 2:
                Upgrade(playerData.weapons[num3]);
                break;
        }
        Close();
    }
    private void Upgrade(IWeapon weapon)
    {
        if (weapon.isActive) { 
            if(weapon.Level < 5)
            {
                weapon.Upgrade();
            }

        }
        else
        {
            weapon.Activate();
        }
        upgradeCount++;
    }

}
