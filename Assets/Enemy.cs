using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    [SerializeField] private EnemyMovement enemyMovement;
    float freezeTimer = 0;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<PlayerData>().Damage(10f);
        }
    }


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
            if (!GlobalData.won)
            {
                GlobalData.reward += 10;
                GlobalData.tempEXP += 5;
            }
            

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
