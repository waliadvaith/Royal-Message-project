using UnityEngine;

public class BoltScript : MonoBehaviour
{
    public float speed = 15f;
    public float damage = 20f;
    public float lifetime = 3f;

    void Start()
    {
        // Clean up the block so the scene doesn't get messy
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Moves forward (to the right of the block)
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Use your "Negative Damage" logic if you want it to heal!
        // But for now, let's stick to damaging enemies.
        if (other.CompareTag("Enemy"))
        {
            Health enemyHP = other.GetComponent<Health>() ?? other.GetComponentInParent<Health>();
            if (enemyHP != null)
            {
                enemyHP.TakeDamage(damage);
            }
            Destroy(gameObject);// Poof on hit
        }


    }
}