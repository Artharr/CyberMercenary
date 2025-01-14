using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PistolPlayer : MonoBehaviour, IWeapon
{
    public GameObject bulletPrefabLvl1;
    public GameObject bulletPrefabLvl2;
    public GameObject bulletPrefabLvl3;
    private float detectionRadius = 50f;
    private float fireRate = 2f;
    private float shootTimmer = 0f;
    private bool isActive = false;
    private int level = 1;

    void Start()
    {
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)&& !isActive)
        {
            Activate();
        }
        if (!isActive) return;
        shootTimmer += Time.deltaTime;

        if (shootTimmer >= fireRate)
        {
            Shoot();
            shootTimmer = 0f;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Upgrade();
        }
    }

    private void Shoot()
    {
        GameObject nearestEnemy = FindNearestEnemy();
        Quaternion rotation;
        if (nearestEnemy is null)
        {
            rotation = transform.rotation;   
        }
        else
        {
            Vector3 direction = (nearestEnemy.transform.position - transform.position).normalized;
            rotation = Quaternion.LookRotation(direction);
        }
        GameObject bullet = Instantiate(BulletBasedOnLevel(), transform.position+Vector3.up, rotation);
    }

    private GameObject BulletBasedOnLevel()
    {
        switch (level)
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

    private GameObject FindNearestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        GameObject nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = collider.gameObject;
                }
            }
        }

        return nearestEnemy;
    }

    public void Activate()
    {
        isActive = true;
    }
    public void Upgrade()
    {
        level++;
        switch (level)
        {
            case 2:
                fireRate = 1.5f;
                return;
            case 3:
                return;
            case 4:
                fireRate = 1f;
                return;
        }
    }
}
