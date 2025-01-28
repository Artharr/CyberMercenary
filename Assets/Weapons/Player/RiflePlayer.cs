using UnityEngine;

public class RiflePlayer : MonoBehaviour, IWeapon
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject bulletPrefabLvl1;
    public GameObject bulletPrefabLvl2;
    public GameObject bulletPrefabLvl3;
    private float fireRate = 5f;
    private float burstfireRate = 0.5f;
    private float ammoInButst = 3f;
    private float ammoUsed = 0f;
    private float shootTimmer = 0f;
    public bool isActive { get; set; } = false;
    public int Level { get; set; } = 1;
    public string Name { get; set; } = "Rifle";

    //void Start()
    //{
    //}
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.X)&& !isActive)
        //{
        //    Activate();
        //}
        if (!isActive) return;
        shootTimmer += Time.deltaTime;

        if (shootTimmer >= fireRate)
        {
            if (shootTimmer >= burstfireRate * ammoUsed + fireRate)
            {
                Shoot();
                ammoUsed++;
                if (ammoUsed >= ammoInButst)
                {
                    ammoUsed = 0f;
                    shootTimmer = 0f;
                }
            }
        }
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    Upgrade();
        //}
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(BulletBasedOnLevel(), transform.position+Vector3.up, transform.rotation);
    }
    private GameObject BulletBasedOnLevel()
    {
        switch (Level)
        {
            case 1:
                return bulletPrefabLvl1;
            case 2:
                return bulletPrefabLvl1;
            case 3:
                return bulletPrefabLvl2;
            case 4:
                return bulletPrefabLvl2;
            case 5:
                return bulletPrefabLvl3;
        }
        return null;
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Upgrade()
    {
        Level++;
        switch (Level)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                ammoInButst = 5f;
                break;
            case 4:
                break;
            case 5:
                fireRate = 0f;
                break;
        }
    }
}
