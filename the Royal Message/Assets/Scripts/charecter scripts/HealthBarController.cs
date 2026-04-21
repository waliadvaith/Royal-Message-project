using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Gradient gradient;
    public Image fill;

    [Header("Flash Settings")]
    public Color flashColor = Color.white;
    public float flashDuration = 0.2f;
    private Coroutine flashRoutine;

    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
        if (fill != null) fill.color = gradient.Evaluate(1f);
    }

    public void UpdateHealthBar(float health)
    {
        // Force health to 0 if it's super low (prevents that "tiny sliver" look)
        if (health <= 0.01f) health = 0;

        healthSlider.value = health;

        // If dead, ensure the fill color is at the very start of the gradient
        if (health <= 0)
        {
            if (flashRoutine != null) StopCoroutine(flashRoutine);
            fill.color = gradient.Evaluate(0f);

            // Optional: Hide the fill completely if it's at zero
            // fill.enabled = false; 
            return;
        }

        if (gameObject.activeInHierarchy)
        {
            if (flashRoutine != null) StopCoroutine(flashRoutine);
            flashRoutine = StartCoroutine(SmoothFlashRoutine());
        }
    }

    private IEnumerator SmoothFlashRoutine()
    {
        float elapsed = 0f;

        // Use normalizedValue (0 to 1) to get the correct color from the gradient
        Color targetColor = gradient.Evaluate(healthSlider.normalizedValue);

        fill.color = flashColor;

        while (elapsed < flashDuration)
        {
            elapsed += Time.deltaTime;
            float lerpPercent = elapsed / flashDuration;
            fill.color = Color.Lerp(flashColor, targetColor, lerpPercent);
            yield return null;
        }

        fill.color = targetColor;
        flashRoutine = null;
    }
}