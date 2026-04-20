using UnityEngine;
using UnityEngine.UI;

public class morality : MonoBehaviour 
{
    // -100 (Evil) to 100 (Good)
    public float currentMorality = 0f;
    public float maxMorality = 100f;

    // Reference to a UI Slider component
    public Slider moralitySlider;

    public void AddMorality(float amount)
    {
        currentMorality += amount;
        currentMorality = Mathf.Clamp(currentMorality, -maxMorality, maxMorality);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (moralitySlider != null)
        {
            // Normalize value to 0-1 range for slider
            moralitySlider.value = (currentMorality + maxMorality) / (2 * maxMorality);
        }
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object other)
    {
        return base.Equals(other);
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public morality(float currentMorality, float maxMorality, Slider moralitySlider)
    {
        this.currentMorality = currentMorality;
        this.maxMorality = maxMorality;
        this.moralitySlider = moralitySlider;
    }

}
