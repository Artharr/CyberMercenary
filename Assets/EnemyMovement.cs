using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float speed = 3.5f;
    GameObject player;
    public bool isFrozen = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        agent.isStopped = isFrozen;
        agent.SetDestination(player.transform.position);
    }
}
