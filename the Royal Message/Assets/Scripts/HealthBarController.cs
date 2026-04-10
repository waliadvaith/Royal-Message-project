using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Gradient gradient;
    public Image fill; // Drag the "Fill" object from the Slider here

    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;


        fill.color = gradient.Evaluate(1f);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthSlider.value = currentHealth;


        fill.color = gradient.Evaluate(healthSlider.normalizedValue);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }
}