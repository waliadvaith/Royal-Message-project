using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float stopDistance = 1.5f; // Distance where enemy stops to swing
    public float detectionRange = 15.0f;

    private Transform player;
    private SwordScript mySword;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Find the sword script on the child object
        mySword = GetComponentInChildren<SwordScript>();

        // Find the player automatically
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // 1. Move toward the player if they are in range but not too close
        if (distance < detectionRange && distance > stopDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
        // 2. Attack if close enough
        else if (distance <= stopDistance)
        {
            if (mySword != null)
            {
                // This replaces the "Spacebar" for the enemy
                mySword.TryAttack();
            }
        }
    }
}