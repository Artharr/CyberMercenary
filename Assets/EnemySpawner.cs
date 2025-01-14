using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    [SerializeField] float mindelay = 1f;
    [SerializeField] float maxDelay = 5f;
    float delay = 0f;
    [SerializeField] bool canSpawn = true;

    private void Start()
    {
        delay = Random.Range(mindelay, maxDelay);
    }
    void Update()
    {
        if (canSpawn)
        {
            delay -= Time.deltaTime;
            if (delay < 0f)
            {
                Spawn();
                delay = Random.Range(mindelay, maxDelay);
            }
        }
    }

    private void Spawn()
    {
        GameObject EnemyInstance = Instantiate(Enemy, transform);
    }

    private void OnBecameVisible()
    { 
        canSpawn = false;
    }
    private void OnBecameInvisible()
    {
        canSpawn = true;
    }
}
