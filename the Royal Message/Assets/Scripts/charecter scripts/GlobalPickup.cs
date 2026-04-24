using UnityEngine;

public class GlobalPickup : MonoBehaviour
{
    public enum ItemType { HealthPotion, Ammo, Sword }
    public ItemType type;
    public int amount = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HotbarManager hotbar = other.GetComponentInChildren<HotbarManager>();
            if (hotbar != null)
            {
                if (hotbar.AddItemToHotbar(type, amount))
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}