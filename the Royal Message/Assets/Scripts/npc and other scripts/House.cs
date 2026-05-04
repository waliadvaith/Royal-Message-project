using UnityEngine;
using TMPro; // Make sure you have TextMeshPro installed!

public class House : MonoBehaviour
{
    [Header("Loot Settings")]
    public GameObject coinPrefab;    // Drag your Coin Prefab here
    public Transform dropPoint;      // Create an empty child object where coins pop out
    public int minCoins = 3;
    public int maxCoins = 8;
    public int moralityLoss = -5;

    [Header("UI Reference")]
    public GameObject lootPrompt;    // A small Canvas/Text over the house
    public MoralityManager MoralityManager;
    private bool canLoot = false;
    private bool isLooted = false;

    void Start()
    {
        if (lootPrompt != null) lootPrompt.SetActive(false);
    }

    void Update()
    {
        if (canLoot && !isLooted && Input.GetKeyDown(KeyCode.E))
        {
            LootHouse();
        }
    }

    void LootHouse()
    {
        isLooted = true;
        if (lootPrompt != null) lootPrompt.SetActive(false);

        // 1. Reduce Morality
        MoralityManager.AddMorality(moralityLoss);

        // 2. Spawn physical coins
        int amount = Random.Range(minCoins, maxCoins);
        for (int i = 0; i < amount; i++)
        {
            GameObject coin = Instantiate(coinPrefab, dropPoint.position, Quaternion.identity);

            // Add a little "pop" force so they scatter

        }

        // 3. Visual Feedback (Dim the house)
        GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isLooted)
        {
            canLoot = true;
            if (lootPrompt != null) lootPrompt.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canLoot = false;
            if (lootPrompt != null) lootPrompt.SetActive(false);
        }
    }
}