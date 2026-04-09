using UnityEngine;

using UnityEngine.UI;
public class livesleft : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthSlider; // Link to UI Slider

    void Start()
    {
        currentHealth = maxHealth;
        if (healthSlider != null) healthSlider.value = 1f; // 100%
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (healthSlider != null)
            healthSlider.value = currentHealth / maxHealth;

        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        Debug.Log("Player died");
        Destroy(gameObject);
    }
}

