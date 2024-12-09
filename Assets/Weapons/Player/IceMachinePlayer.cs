using UnityEngine;

public class IceMachinePlayer : MonoBehaviour, IWeapon
{
    public GameObject IcePrefab;
    private float fireRate = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Activate();
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(IcePrefab, transform.position, transform.rotation);
    }

    public void Activate()
    {
        InvokeRepeating(nameof(Shoot), 0f, fireRate);
    }

    public void Upgrade()
    {
        throw new System.NotImplementedException();
    }
}
