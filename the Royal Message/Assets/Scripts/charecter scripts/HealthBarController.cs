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
    public float flashDuration = 0.2f; // How long the total fade takes
    private Coroutine flashRoutine;

    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
        if (fill != null) fill.color = gradient.Evaluate(1f);
    }

    public void UpdateHealthBar(float health)
    {
        healthSlider.value = health;

        if (gameObject.activeInHierarchy)
        {
            if (flashRoutine != null) StopCoroutine(flashRoutine);
            flashRoutine = StartCoroutine(SmoothFlashRoutine());
        }
    }

    private IEnumerator SmoothFlashRoutine()
    {
        float elapsed = 0f;
        Color targetColor = gradient.Evaluate(healthSlider.normalizedValue);

        // 1. Immediately snap to flash color
        fill.color = flashColor;

        // 2. Smoothly fade back to the gradient color
        while (elapsed < flashDuration)
        {
            elapsed += Time.deltaTime;
            float lerpPercent = elapsed / flashDuration;

            // This blends between White and your Purple/Red gradient
            fill.color = Color.Lerp(flashColor, targetColor, lerpPercent);

            yield return null;
        }

        // 3. Ensure it lands exactly on the gradient color
        fill.color = targetColor;
        flashRoutine = null;
    }
}
