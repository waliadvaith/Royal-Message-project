using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Settings")]
    public float maxHealth = 999f;
    public float currentHealth;

    [Header("UI Connection")]
    public HealthBar healthBar; // Drag the Slider's HealthBar script here

    [Header("Drops")]
    public GameObject healthPotPrefab; // Drag your Red Block prefab here
    [Range(0, 100)] public float dropChance = 30f; // 30% chance to drop

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

    // --- NEW HEAL FUNCTION ---
    public void Heal(float amount)
    {
        currentHealth += amount;

        // Ensure we don't go over the max
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log(gameObject.name + " healed for " + amount + ". Current Health: " + currentHealth);

        // Update the visual bar so the player sees the green go up!
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Something took damage");

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
            Debug.Log("Player has died. Triggering Death Screen.");
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
            if (gameObject.CompareTag("Enemy"))
            {
                if (Random.Range(0f, 100f) <= dropChance)
                {
                    if (healthPotPrefab != null)
                    {
                        Instantiate(healthPotPrefab, transform.position, Quaternion.identity);
                    }
                }
            }
            Destroy(gameObject);
        }
    }
}