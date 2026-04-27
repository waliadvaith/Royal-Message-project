using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Settings")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI Connection")]
    public HealthBar healthBar;

    [Header("Drops")]
    public GameObject goldPrefab;
    [Range(0, 100)] public float dropChance = 50f;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (gameObject.CompareTag("Player"))
        {
            // Player death logic
            if (GetComponent<Rigidbody2D>() != null)
                GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

            if (GetComponent<CharacterMovement>() != null)
                GetComponent<CharacterMovement>().enabled = false;

            if (GetComponent<SpriteRenderer>() != null)
                GetComponent<SpriteRenderer>().enabled = false;

            if (GetComponent<Collider2D>() != null)
                GetComponent<Collider2D>().enabled = false;

            gameObject.tag = "Untagged";
        }
        else
        {
            // Enemy death logic
            if (gameObject.CompareTag("Enemy"))
            {
                // Roll for gold drop
                if (Random.Range(0f, 100f) <= dropChance)
                {
                    if (goldPrefab != null)
                    {
                        Instantiate(goldPrefab, transform.position, Quaternion.identity);
                    }
                }
            }

            Destroy(gameObject);
        }
    }
}