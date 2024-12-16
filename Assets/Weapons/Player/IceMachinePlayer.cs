using UnityEngine;

public class IceMachinePlayer : MonoBehaviour
{
    public GameObject IcePrefab;
    private float fireRate = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(Shoot), 0f, fireRate);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(IcePrefab, transform.position, transform.rotation);
    }
}
