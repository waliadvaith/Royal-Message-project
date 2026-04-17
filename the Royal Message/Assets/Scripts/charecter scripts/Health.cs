using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Settings")]
    public float maxHealth = 999f;
    public float currentHealth;

    [Header("UI Connection")]
    public HealthBar healthBar; // Drag the Slider's HealthBar script here

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
        Debug.Log(gameObject.name + " was defeated!");
        Destroy(gameObject); // Deletes the enemy and their health bar
    }
}
