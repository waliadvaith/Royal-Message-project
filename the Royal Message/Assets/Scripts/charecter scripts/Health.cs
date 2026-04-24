using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Settings")]
    public float maxHealth = 999f;
    public float currentHealth;

    [Header("UI Connection")]
    public HealthBar healthBar;

    [Header("Drops")]
    public GameObject healthPotPrefab; // Slot for your Red Potion prefab
    public GameObject ammoPrefab;      // NEW: Slot for your Ammo/Bolt prefab
    [Range(0, 100)] public float dropChance = 30f;

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
            // Player death logic (stays the same)
            Debug.Log("Player Died");
            // ... (your existing player death code)
        }
        else
        {
            // --- ENEMY DEATH ---
            // CRITICAL: Make sure the tag is EXACTLY "Enemy" in Unity
            if (gameObject.CompareTag("Enemy"))
            {
                Debug.Log(gameObject.name + " is dropping loot!");

                if (Random.Range(0f, 100f) <= dropChance)
                {
                    GameObject itemToDrop = (Random.value > 0.5f) ? healthPotPrefab : ammoPrefab;

                    if (itemToDrop != null)
                    {
                        // Spawn it slightly above the enemy so it doesn't get stuck in the floor
                        Vector3 spawnPos = transform.position + new Vector3(0, 0.2f, 0);
                        Instantiate(itemToDrop, spawnPos, Quaternion.identity);
                    }
                    else
                    {
                        Debug.LogWarning("Drop roll succeeded but Prefab is missing in Inspector!");
                    }
                }
            }

            // Only ONE destroy call at the very end
            Destroy(gameObject);
        }
    }
}