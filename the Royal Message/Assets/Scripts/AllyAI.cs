using UnityEngine;

public class AllyAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float attackRange = 1.5f;
    public string enemyTag = "Enemy";

    private Transform target;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Check for targets every half second so we don't lag the game
        InvokeRepeating("FindNearestEnemy", 0f, 0.5f);
    }

    void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }
        target = closestEnemy;
    }

    void FixedUpdate()
    {
        if (target == null) return;

        float distance = Vector2.Distance(rb.position, target.position);

        if (distance > attackRange)
        {
            Vector2 direction = ((Vector2)target.position - rb.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }
}