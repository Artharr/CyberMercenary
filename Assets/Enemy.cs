using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    [SerializeField] private EnemyMovement enemyMovement;
    float freezeTimer = 0;

    // Update is called once per frame
    void Update()
    {
        if (freezeTimer > 0)
        {
            freezeTimer -= Time.deltaTime;
            if (freezeTimer < 0)
            {
                Unfreeze();
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP < 0)
        {
            Destroy(gameObject);
        }
    }
    public void Freeze(float duration)
    {
        enemyMovement.isFrozen = true;
        freezeTimer = duration;
    }
    private void Unfreeze()
    {
        enemyMovement.isFrozen = false;
    }
}
