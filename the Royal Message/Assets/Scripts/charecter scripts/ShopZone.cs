using UnityEngine;

public class TraderShopTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the thing that touched the trader is the Player
        if (other.CompareTag("Player"))
        {
            // Look for the Hotbar script. 
            // It might be on the Player object or a Child object.
            HotbarManager hotbar = other.GetComponent<HotbarManager>();

            // If it's not on the main object, look in the children
            if (hotbar == null) hotbar = other.GetComponentInChildren<HotbarManager>();

            if (hotbar != null)
            {
                hotbar.SetWeaponsActive(false);
                Debug.Log("Player entered shop: Weapons holstered.");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HotbarManager hotbar = other.GetComponent<HotbarManager>();
            if (hotbar == null) hotbar = other.GetComponentInChildren<HotbarManager>();

            if (hotbar != null)
            {
                hotbar.SetWeaponsActive(true);
                Debug.Log("Player left shop: Weapons drawn.");
            }
        }
    }
}