using UnityEngine;
using UnityEngine.AI; // MUST HAVE THIS

public class EnemyAI : MonoBehaviour
{
    public Transform player;      // Drag Player here
    public int damageAmount = 10;
    public float attackSpeed = 1.5f;

    private NavMeshAgent agent;
    private float nextAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

     
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (player != null)
        {
         
            agent.SetDestination(player.position);
        }
    }


}