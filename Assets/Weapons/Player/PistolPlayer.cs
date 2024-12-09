using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PistolPlayer : MonoBehaviour, IWeapon
{
    public GameObject bulletPrefab;
    private float detectionRadius = 50f;
    private float fireRate = 2f;
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
            Shoot();
            shootTimmer = 0f;
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
        GameObject bullet = Instantiate(bulletPrefab, transform.position+Vector3.up, rotation);
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
        throw new System.NotImplementedException();
    }
}
