using System;
using UnityEngine;

public class TurretPlayer : MonoBehaviour, IWeapon
{
    public GameObject turretPrefabLvl1;
    public GameObject turretPrefabLvl2;
    public GameObject turretPrefabLvl3;
    private float fireRate = 10f;
    //private bool isActive = false;
    public bool isActive { get; set; } = false;
    public int Level { get; set; } = 1;
    public string Name { get; set; } = "Turret";

    //void Start()
    //{
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.V)&& !isActive)
    //    {
    //        Activate();
    //    }
    //    if (Input.GetKeyDown(KeyCode.P))
    //    {
    //        Upgrade();
    //    }
    //}

    // Update is called once per frame
    private void CreateTurret()
    {
        GameObject turret = Instantiate(BulletBasedOnLevel(), transform.position, transform.rotation);
    }
    private GameObject BulletBasedOnLevel()
    {
        if (Level < 5)
        {
            switch (Level)
            {
                case 1:
                    return turretPrefabLvl1;
                case 2:
                    return turretPrefabLvl1;
                case 3:
                    return turretPrefabLvl2;
                case 4:
                    return turretPrefabLvl2;
                case 5:
                    return turretPrefabLvl3;
            }
        }
        else
        {
            return turretPrefabLvl3;
        }
        return turretPrefabLvl1;

    }

    public void Activate()
    {
        isActive = true;
        InvokeRepeating("CreateTurret", 0f, fireRate);
    }

    public void Upgrade()
    {
        Level++;
        switch (Level)
        {
            case 1:
                break;
            case 2:
                fireRate = 7f;
                break;
            case 3:
                break;
            case 4:
                fireRate = 5f;
                break;
            case 5:
                break;
        }
    }
}
