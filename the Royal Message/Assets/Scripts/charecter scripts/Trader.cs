using UnityEngine;
using UnityEngine.UI;

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
    public Button closeMenuBtn; // Added a close button reference

    private HotbarUI ui;

    void Start()
    {
        if (floatingMenu != null) floatingMenu.SetActive(false);

        if (buyAmmoBtn != null) buyAmmoBtn.onClick.AddListener(BuyAmmo);
        if (buyPotBtn != null) buyPotBtn.onClick.AddListener(BuyPotion);

        // Setup Close Button
        if (closeMenuBtn != null) closeMenuBtn.onClick.AddListener(CloseTraderMenu);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ui = Object.FindFirstObjectByType<HotbarUI>();
            OpenTraderMenu();
        }
    }

    // This won't trigger while Time.timeScale is 0! 
    // We keep it here as a safety fallback.
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CloseTraderMenu();
        }
    }

    void OpenTraderMenu()
    {
        if (floatingMenu != null) floatingMenu.SetActive(true);

        // 1. Freeze Time
        Time.timeScale = 0f;

        // 2. Cursor Setup
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseTraderMenu()
    {
        if (floatingMenu != null) floatingMenu.SetActive(false);

        // 1. Resume Time
        Time.timeScale = 1f;

        // 2. Lock Cursor back
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void BuyPotion()
    {
        if (ui != null && ui.goldCount >= potionPrice)
        {
            ui.goldCount -= potionPrice;
            ui.RefreshUI();
            // Note: Instantiate still works while time is frozen!
            Instantiate(potionPrefab, dropPoint.position, Quaternion.identity);
        }
    }

    public void BuyAmmo()
    {
        if (ui != null && ui.goldCount >= ammoPrice)
        {
            ui.goldCount -= ammoPrice;
            ui.RefreshUI();
            Instantiate(ammoPrefab, dropPoint.position, Quaternion.identity);
        }
    }
}