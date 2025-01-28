using System;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]private float speed = 25f;
    private float lifetime = 5f;
    public int damage = 20;
    private GameData gameData;
    void Start()
    {
        gameData = SaveSystem.LoadGame();
        damage += gameData.damageLevel;
        speed += gameData.attackSpeedLevel;
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponentInParent<Enemy>();
            enemy.TakeDamage(damage);
            //Debug.Log($"{other.name} collided with bullet");

            Destroy(gameObject);
        }
    }
}
