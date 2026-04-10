using UnityEngine;
using UnityEngine.UI; 
public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;

       
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    // Call this method whenever the player takes damage
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        // Clamp health so it doesn't go below 0
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update the UI
        healthSlider.value = currentHealth;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }
}