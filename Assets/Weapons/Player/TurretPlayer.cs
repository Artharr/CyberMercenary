using System;
using UnityEngine;

public class TurretPlayer : MonoBehaviour, IWeapon
{
    public GameObject turretPrefabLvl1;
    public GameObject turretPrefabLvl2;
    public GameObject turretPrefabLvl3;
    private float fireRate = 10f;
    private int level = 1;
    private bool isActive = false;
    void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)&& !isActive)
        {
            Activate();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Upgrade();
        }
    }

    // Update is called once per frame
    private void CreateTurret()
    {
        GameObject turret = Instantiate(BulletBasedOnLevel(), transform.position, transform.rotation);
    }
    private GameObject BulletBasedOnLevel()
    {
        switch (level)
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
        return null;
    }

    public void Activate()
    {
        isActive = true;
        InvokeRepeating("CreateTurret", 0f, fireRate);
    }

    public void Upgrade()
    {
        level++;
        switch (level)
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
