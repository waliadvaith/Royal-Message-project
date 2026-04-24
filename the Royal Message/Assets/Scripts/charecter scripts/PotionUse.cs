using UnityEngine;

public class PotionUse : MonoBehaviour
{
    public float healAmount = 25f;
    public int potionCount = 0;

    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        Debug.Log("Equipped Potion. Current count: " + potionCount);
    }

    void Update()
    {
        // 1. Visually show/hide
        if (sr != null)
        {
            sr.enabled = (potionCount > 0);
        }

        // 2. Input Check
        if (Input.GetMouseButtonDown(0))
        {
            if (potionCount > 0)
            {
                Drink();
            }
            else
            {
                Debug.Log("Tried to drink, but potionCount is 0!");
            }
        }
    }

    void Drink()
    {
        // Find health on the parent (Player)
        Health h = transform.root.GetComponentInChildren<Health>();

        if (h != null)
        {
            h.Heal(healAmount);
            potionCount--;
            Debug.Log("Gulp! Healed " + healAmount + ". Remaining: " + potionCount);
        }
        else
        {
            Debug.LogError("Potion could not find the Health script on the Player!");
        }
    }
}