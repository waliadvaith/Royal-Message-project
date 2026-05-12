using UnityEngine;
using TMPro;

public class HouseLoot : MonoBehaviour
{
    [Header("Loot Settings")]
    public GameObject coinPrefab;
    public Transform dropPoint;
    public int minCoins = 3;
    public int maxCoins = 8;
    public int moralityLoss = -5;

    [Header("UI Reference")]
    public TextMeshProUGUI promptText;
    public MoralityManager MoralityManager;

    private bool canLoot = false;
    private bool isLooted = false;
    private bool isLocked = false;
    private bool hasGeneratedLockStatus = false;

    void Start()
    {
        if (promptText != null) promptText.gameObject.SetActive(false);

        // Roll for lock status ONCE when the house is created
        DetermineLockStatus();
    }

    void DetermineLockStatus()
    {
        // Multiplied by 2: so -50 becomes 100%
        float lockChance = MoralityManager.MoralityScore < 0 ? Mathf.Abs(MoralityManager.MoralityScore) * 1.5f : 0;

        if (Random.Range(0, 100) < lockChance)
        {
            isLocked = true;
        }
        else
        {
            isLocked = false;
        }

        hasGeneratedLockStatus = true;
    }

    void Update()
    {
        if (canLoot && !isLooted && !isLocked && Input.GetKeyDown(KeyCode.E))
        {
            LootHouse();
        }

    }

    void LootHouse()
    {
        isLooted = true;
        if (promptText != null) promptText.gameObject.SetActive(false);

        // Reference to the updated MoralityScore
        MoralityManager.AddMorality(moralityLoss);

        int amount = Random.Range(minCoins, maxCoins);
        for (int i = 0; i < amount; i++)
        {
            // Simple spawn (No Rigidbody forces)
            Instantiate(coinPrefab, dropPoint.position, Quaternion.identity);
        }

        GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isLooted)
        {
            canLoot = true;

            if (isLocked)
            {
                promptText.text = "LOCKED!";
                promptText.color = Color.red;
            }
            else
            {
                promptText.text = "PRESS E TO LOOT";
                promptText.color = Color.white;
            }

            promptText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canLoot = false;
            if (promptText != null) promptText.gameObject.SetActive(false);
        }
    }
}