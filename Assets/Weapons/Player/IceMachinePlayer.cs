using UnityEngine;

public class IceMachinePlayer : MonoBehaviour, IWeapon
{
    public GameObject IcePrefabLvl1;
    public GameObject IcePrefabLvl2;
    public GameObject IcePrefabLvl3;
    public GameObject IcePrefabLvl4;
    public GameObject IcePrefabLvl5;
    private float fireRate = 5f;
    private int level = 1;
    bool isActive = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)&& !isActive)
        {
            Activate();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Upgrade();
        }
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(BulletBasedOnLevel(), transform.position, transform.rotation);
    }

    private GameObject BulletBasedOnLevel()
    {
        switch (level)
        {
            case 1:
                return IcePrefabLvl1;
            case 2:
                return IcePrefabLvl2;
            case 3:
                return IcePrefabLvl3;
            case 4:
                return IcePrefabLvl4;
            case 5:
                return IcePrefabLvl5;
        }
        return null;
    }
    public void Activate()
    {
        InvokeRepeating(nameof(Shoot), 0f, fireRate);
    }

    public void Upgrade()
    {
        level++;
    }
}
