using UnityEngine;
using TMPro;

public class HotbarUI : MonoBehaviour
{
    [Header("TMP Text Slots")]
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI potionText;

    [Header("Player Script Slots")]
    public CrossbowScript crossbow;
    public PotionUse potion;

    void Update()
    {
        // Update Ammo
        if (crossbow != null && ammoText != null)
        {
            ammoText.text = "Ammo: " + crossbow.currentAmmo;
        }

        // Update Potion
        if (potion != null && potionText != null)
        {
            potionText.text = "Pots: " + potion.potionCount;
        }
    }
}