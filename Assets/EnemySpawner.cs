using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    float minDelay = 1f;
    float maxDelay = 2f;
    float delay = 0f;
    [SerializeField] bool canSpawn = true;


    public void SetDifficulty(int difficultyLevel)
    {
        switch (difficultyLevel)
        {
            case 9:
                minDelay = 3f;
                maxDelay = 6f;
                break;
            case 8:
                minDelay = 2f;
                maxDelay = 5f;
                break;
            case 7:
                minDelay = 2f;
                maxDelay = 3f;
                break;
            case 6:
                minDelay = 1f;
                maxDelay = 3f;
                break;
            case 5:
                minDelay = 1f;
                maxDelay = 2f;
                break;
        }
    }

    private void Start()
    {
        delay = Random.Range(minDelay, maxDelay);
    }
    void Update()
    {
        if (canSpawn)
        {
            delay -= Time.deltaTime;
            if (delay < 0f)
            {
                Spawn();
                delay = Random.Range(minDelay, maxDelay);
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
