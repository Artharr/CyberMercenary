using UnityEngine;

public class RiflePlayer : MonoBehaviour, IWeapon
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject bulletPrefab;
    private float fireRate = 5f;
    private float burstfireRate = 0.5f;
    private float ammoInButst = 3f;
    private float ammoUsed = 0f;
    private float shootTimmer = 0f;
    private bool isActive = false;

    void Start()
    {
        Activate();
    }
    void Update()
    {
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
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position+Vector3.up, transform.rotation);
    }
    

    public void Activate()
    {
        isActive = true;
    }

    public void Upgrade()
    {
        throw new System.NotImplementedException();
    }
}
