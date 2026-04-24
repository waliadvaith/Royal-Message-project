using UnityEngine;
using UnityEngine.UI;

public class morality : MonoBehaviour 
{
    [Header("Morality Settings")]
    public float currentMorality = 0f; // 0 is neutral, -100 evil, 100 good
    public float maxMorality = 100f;
    public float decayRate = 1f; // How fast it returns to 0 per second
    public float smoothSpeed = 5f; // How fast the slider moves

    [Header("UI Components")]
    public Slider moralitySlider;

    private float _targetSliderValue;

    void Start()
    {
        // Set slider range from 0 to 1 for normalized values
        if (moralitySlider != null)
        {
            moralitySlider.minValue = 0f;
            moralitySlider.maxValue = 1f;
        }
        UpdateSliderTarget();
        // Set initial slider value instantly
        if (moralitySlider != null) moralitySlider.value = _targetSliderValue;
    }

    void Update()
    {
        // 1. Auto-decay toward neutral (0) over time
        if (currentMorality != 0)
        {
            // Moves currentMorality towards 0
            currentMorality = Mathf.MoveTowards(currentMorality, 0, decayRate * Time.deltaTime);
            UpdateSliderTarget();
        }

        // 2. Smoothly update the slider UI
        if (moralitySlider != null)
        {
            moralitySlider.value = Mathf.Lerp(moralitySlider.value, _targetSliderValue, Time.deltaTime * smoothSpeed);
        }
    }

    // Call this to change morality (positive for good, negative for evil)
    public void ChangeMorality(float amount)
    {
        currentMorality += amount;
        currentMorality = Mathf.Clamp(currentMorality, -maxMorality, maxMorality);
        UpdateSliderTarget();
    }

    // Maps -100/100 range to 0/1 for slider
    void UpdateSliderTarget()
    {
        _targetSliderValue = (currentMorality + maxMorality) / (2 * maxMorality);
    }


}
