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
        // Set health before anything else happens
        currentHealth = maxHealth;
    }

    void Start()
    {
        // Tell the UI what the max health is
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Something took damage");

        // Update the visual bar
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
            // --- PLAYER DEATH (Soft Death) ---
            Debug.Log("Player has died. Triggering Death Screen.");

            // 1. Stop Movement (prevents sliding after death)
            if (GetComponent<Rigidbody2D>() != null)
                GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

            // 2. Disable Controls and Scripts
            // (Replace 'PlayerController' with your actual movement script name)
            if (GetComponent<CharacterMovement>() != null)
                GetComponent<CharacterMovement>().enabled = false;

            // 3. Hide the character but keep the object alive
            if (GetComponent<SpriteRenderer>() != null)
                GetComponent<SpriteRenderer>().enabled = false;

            // 4. Disable Colliders so enemies stop bumping into you
            if (GetComponent<Collider2D>() != null)
                GetComponent<Collider2D>().enabled = false;

            // 5. Change Tag so enemies stop trying to attack you
            gameObject.tag = "Untagged";

            // 6. Show the Death Screen UI
            // deathScreenUI.SetActive(true);
        }
        else
        {
            // --- ENEMY/OTHER DEATH (Instant Destruction) ---
            if (gameObject.CompareTag("Enemy"))
            {
                // Roll for a drop
                if (Random.Range(0f, 100f) <= dropChance)
                {
                    if (healthPotPrefab != null)
                    {
                        Instantiate(healthPotPrefab, transform.position, Quaternion.identity);
                    }
                }

                Destroy(gameObject);
            }
            Destroy(gameObject);

        }
    }
}
