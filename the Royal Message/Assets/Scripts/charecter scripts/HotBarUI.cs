using UnityEngine;
using TMPro;

public class HotbarUI : MonoBehaviour
{
    [Header("TMP Text Slots")]
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI potionText;
    public TextMeshProUGUI goldText;

    [Header("Values")]
    public int goldCount = 0;

    // Use [SerializeField] so you can drag them in manually if "Find" fails
    [Header("Script References")]
    public CrossbowScript crossbow;
    public PotionUse potion;

    void Start()
    {
        // Try to find them if they weren't dragged into the inspector
        if (crossbow == null) crossbow = FindFirstObjectByType<CrossbowScript>();
        if (potion == null) potion = FindFirstObjectByType<PotionUse>();

        RefreshUI();
    }

    void Update()
    {
        // Keep text in sync
        RefreshUI();
    }

    public void AddGold(int amount)
    {
        goldCount += amount;
        RefreshUI();
    }

    public void RefreshUI()
    {
        if (goldText != null)
            goldText.text = goldCount + " rockets on king tower: ";

        if (crossbow != null && ammoText != null)
            ammoText.text = "Ammo: " + crossbow.currentAmmo;

        if (potion != null && potionText != null)
            potionText.text = "Pots: " + potion.potionCount;
    }
}