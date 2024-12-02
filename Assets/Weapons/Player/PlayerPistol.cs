using UnityEngine;

public class Pistol : MonoBehaviour
{
    //script added to capsule not to player becouse of rotation
    public GameObject bulletPrefab;
    private float fireRate = 2f;
    void Start()
    {
        InvokeRepeating(nameof(Shoot), 0f, fireRate);
    }

    void Shoot()
    {
        Quaternion rotation = transform.rotation * Quaternion.Euler(90, 0, 0);   
        GameObject bullet = Instantiate(bulletPrefab, transform.position+Vector3.up, rotation);
    }
}
