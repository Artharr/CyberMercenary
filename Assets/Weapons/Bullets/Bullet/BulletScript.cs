using System;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private float speed = 10f;
    private float lifetime = 5f;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log($"{other.name} collided with bullet");
            Destroy(gameObject);
        }
    }
}
