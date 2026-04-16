using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;

        // Sets initial color to the far right of the gradient (Green)
        if (fill != null) fill.color = gradient.Evaluate(1f);
    }

    public void UpdateHealthBar(float health)
    {
        healthSlider.value = health;

        // Changes color based on how much health is left
        if (fill != null)
            fill.color = gradient.Evaluate(healthSlider.normalizedValue);
    }
}
