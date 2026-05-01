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

        if (MoralityManager.Instance != null)
        {
            float currentScore = MoralityManager.Instance.MoralityScore;
            moralitySlider.value = currentScore;
        }

        if (fill != null)
        {
            fill.color = gradient.Evaluate(moralitySlider.normalizedValue);
            fill.enabled = (moralitySlider.value > moralitySlider.minValue);
        }
    }

}