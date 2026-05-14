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
            goldText.text = "Money: "+goldCount;

        if (crossbow != null && ammoText != null)
            ammoText.text = "Ammo: " + crossbow.currentAmmo;

        if (potion != null && potionText != null)
            potionText.text = "Pots: " + potion.potionCount;
    }
    void Awake()
    {
        // Using the modern Unity 2023+ method to avoid warnings
        GameObject[] allObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (GameObject go in allObjects)
        {
            // Check if it has the same name but isn't THIS specific instance
            if (go.name == gameObject.name && go != gameObject)
            {
                // If the other object is already 'Global' (DontDestroyOnLoad), it's the old one.
                // We want to keep the old one and destroy this new duplicate.
                Destroy(gameObject);
                return;
            }
        }
    }
}