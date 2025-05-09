using UnityEngine;

public class IceMachineScript : MonoBehaviour
{
    private float lifetime = 0.2f;
    public float freezeTime = 1f;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Debug.Log($"{other.name} is frozen");
            Enemy enemy = other.gameObject.GetComponentInParent<Enemy>();
            enemy.Freeze(freezeTime);
        }
    }
}

