using UnityEngine;
using UnityEngine.UI;

public class MoralityBar : MonoBehaviour
{
    public Slider moralitySlider;
    public Gradient gradient;
    public Image fill;

    [Header("Settings")]
    public int minMorality = -50;
    public int maxMorality = 50;

    void Start()
    {
        // Setup slider limits
        moralitySlider.minValue = minMorality;
        moralitySlider.maxValue = maxMorality;
    }

    void Update()
    {
        // Sync the slider with the Global Morality Score
        float currentScore = MoralityManager.MoralityScore;
        moralitySlider.value = currentScore;

        // Update the color based on the position (Red for evil, Green for good)
        if (fill != null)
        {
            // normalizedValue converts the score (like -50 to 50) to 0 to 1 for the gradient
            fill.color = gradient.Evaluate(moralitySlider.normalizedValue);

            // Hide if it hits the absolute bottom (optional)
            fill.enabled = (moralitySlider.value > moralitySlider.minValue);
        }
    }
}