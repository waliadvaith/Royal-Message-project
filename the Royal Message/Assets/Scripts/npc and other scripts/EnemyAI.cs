using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 1.2f;

    private Transform playerTransform;
    private Rigidbody2D rb;
    private SwordScript mySword;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mySword = GetComponentInChildren<SwordScript>();

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) playerTransform = p.transform;
    }

    void FixedUpdate()
    {
        if (playerTransform == null) return;

        float distance = Vector2.Distance(transform.position, playerTransform.position);

        // Move toward player
        if (distance > attackRange * 0.9f)
        {
            Vector2 dir = ((Vector2)playerTransform.position - rb.position).normalized;
            rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
        }

        // Sword Logic
        if (mySword != null)
        {
            // Face the player
            float side = (playerTransform.position.x < transform.position.x) ? -0.5f : 0.5f;
            float yRot = (playerTransform.position.x < transform.position.x) ? 0 : 180;
            mySword.transform.localPosition = new Vector3(side, 0, 0);
            mySword.transform.localRotation = Quaternion.Euler(0, yRot, -90f);

            // Attack if in range
            if (distance <= attackRange)
            {
                mySword.TryAttack();
            }
        }
    }
}