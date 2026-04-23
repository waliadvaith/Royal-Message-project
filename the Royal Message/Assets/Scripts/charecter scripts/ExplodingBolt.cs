using UnityEngine;

public class ExplodingBolt : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 20f;

    [Header("Explosion Settings")]
    public float explosionDamage = 100f;
    public float explosionRadius = 3f;
    public string targetTag = "Enemy";

    [Header("Visuals")]
    // Drag your downloaded explosion prefab here!
    public GameObject explosionEffect;

    private Rigidbody2D rb;
    private bool hasExploded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Applying movement using the modern linearVelocity
        rb.linearVelocity = transform.right * speed;

        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasExploded || other.CompareTag("Player")) return;
        Explode();
    }

    void Explode()
    {
        hasExploded = true;

        // SPAWN THE VISUAL EFFECT
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Area of Effect Damage
        Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D obj in objectsInRange)
        {
            if (obj.CompareTag(targetTag))
            {
                Health enemyHealth = obj.GetComponent<Health>() ?? obj.GetComponentInParent<Health>();
                if (enemyHealth != null) enemyHealth.TakeDamage(explosionDamage);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}