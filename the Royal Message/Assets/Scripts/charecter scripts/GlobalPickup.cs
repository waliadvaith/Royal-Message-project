using UnityEngine;

public class GlobalPickup : MonoBehaviour
{
    public enum ItemType { Ammo, HealthPotion, Gold }

    [Header("Pickup Settings")]
    public ItemType type;
    public int amount = 5;

    [Header("Visuals (Optional)")]
    public bool destroyOnPickup = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Make sure only the player can pick things up
        if (other.CompareTag("Player"))
        {
            bool pickedUp = false;

            switch (type)
            {
                case ItemType.Gold:
                    // Find the UI script to add gold directly
                    HotbarUI ui = Object.FindFirstObjectByType<HotbarUI>();
                    if (ui != null)
                    {
                        ui.AddGold(amount);
                        pickedUp = true;
                    }
                    break;

                case ItemType.Ammo:
                    // Find the Crossbow to add ammo
                    CrossbowScript crossbow = other.GetComponentInChildren<CrossbowScript>(true);
                    if (crossbow != null)
                    {
                        crossbow.AddAmmo(amount);
                        pickedUp = true;
                    }
                    break;

                case ItemType.HealthPotion:
                    // Find the Potion script to add to the count
                    PotionUse potion = other.GetComponentInChildren<PotionUse>(true);
                    if (potion != null)
                    {
                        potion.potionCount += amount;
                        pickedUp = true;
                    }
                    break;
            }

            if (pickedUp && destroyOnPickup)
            {
                // Play a sound effect here if you have one!
                Destroy(gameObject);
            }
        }
    }
}