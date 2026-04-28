using UnityEngine;
using UnityEngine.UI; // Required for Button logic

public class Trader : MonoBehaviour
{
    [Header("Prices")]
    public int potionPrice = 10;
    public int ammoPrice = 5;

    [Header("Prefabs to Drop")]
    public GameObject potionPrefab;
    public GameObject ammoPrefab;
    public Transform dropPoint;

    [Header("UI Menu (Floating)")]
    public GameObject floatingMenu;
    public Button buyAmmoBtn;
    public Button buyPotBtn;

    private HotbarUI ui;

    void Start()
    {
        if (floatingMenu != null) floatingMenu.SetActive(false);

        // Link the buttons via code so you don't have to drag them 
        if (buyAmmoBtn != null) buyAmmoBtn.onClick.AddListener(BuyAmmo);
        if (buyPotBtn != null) buyPotBtn.onClick.AddListener(BuyPotion);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ui = Object.FindFirstObjectByType<HotbarUI>();
            if (floatingMenu != null) floatingMenu.SetActive(true);

            // Allow clicking
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (floatingMenu != null) floatingMenu.SetActive(false);

            // Go back to game mode
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void BuyPotion()
    {
        if (ui != null && ui.goldCount >= potionPrice)
        {
            ui.goldCount -= potionPrice;
            ui.RefreshUI();
            Instantiate(potionPrefab, dropPoint.position, Quaternion.identity);
            Debug.Log("Potion dropped!");
        }
    }

    public void BuyAmmo()
    {
        if (ui != null && ui.goldCount >= ammoPrice)
        {
            ui.goldCount -= ammoPrice;
            ui.RefreshUI();
            Instantiate(ammoPrefab, dropPoint.position, Quaternion.identity);
            Debug.Log("Ammo dropped!");
        }
    }
}