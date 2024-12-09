using UnityEngine;

public class TurretPlayer : MonoBehaviour, IWeapon
{
    public GameObject turretPrefab;
    private float fireRate = 10f;
    void Start()
    {
        Activate();
    }

    // Update is called once per frame
    private void CreateTurret()
    {
        GameObject turret = Instantiate(turretPrefab, transform.position, transform.rotation);
    }


    public void Activate()
    {
        InvokeRepeating("CreateTurret", 0f, fireRate);
    }

    public void Upgrade()
    {
        throw new System.NotImplementedException();
    }
}
