using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Settings")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Flee Settings")]
    [Range(0, 100)] public float fleeThresholdPercent = 20f;
    [Range(0, 100)] public float fleeChance = 50f;
    private bool hasFled = false;

    [Header("UI Connection")]
    public HealthBar healthBar;

    [Header("Drops")]
    public GameObject goldPrefab;
    [Range(0, 100)] public float dropChance = 50f;

    void Awake() => currentHealth = maxHealth;

    void Start()
    {
        if (healthBar != null) healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        if (gameObject.CompareTag("Player") || gameObject.name.Contains("Player")) // Name check as a backup
        {
            // 1. Reset Health
            currentHealth = maxHealth;

            // 2. Ensure everything is turned ON
            if (GetComponent<CharacterMovement>() != null) GetComponent<CharacterMovement>().enabled = true;
            if (GetComponent<SpriteRenderer>() != null) GetComponent<SpriteRenderer>().enabled = true;
            if (GetComponent<Collider2D>() != null) GetComponent<Collider2D>().enabled = true;

            // 3. FIX THE TAG: If it was changed to Untagged, change it back
            gameObject.tag = "Player";
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if (healthBar != null) healthBar.UpdateHealthBar(currentHealth);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (healthBar != null) healthBar.UpdateHealthBar(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            CheckForFlee();
        }
    }

    void CheckForFlee()
    {
        // Only run flee logic if it's an enemy with EnemyAI and hasn't fled yet
        if (hasFled) return;

        EnemyAI ai = GetComponent<EnemyAI>();
        if (ai == null) return; // This ignores Traders and Players

        if ((currentHealth / maxHealth) * 100 <= fleeThresholdPercent)
        {
            if (Random.Range(0f, 100f) <= fleeChance)
            {
                hasFled = true;
                ai.StartFleeing();
            }
        }
    }

    void Die()
    {
        if (gameObject.CompareTag("Player"))
        {


            EndGameScreens screens = GetComponentInChildren<EndGameScreens>();
            if (screens != null) screens.ActivateLose();

        }
        else
        {
            // CRUELTY CHECK: If player kills a fleeing enemy
            if (hasFled)
            {
                Object.FindFirstObjectByType<MoralityManager>()?.AddMorality(-5);
                Debug.Log("Cruel kill! Morality decreased.");
            }

            if (gameObject.CompareTag("Enemy") && !hasFled)
            {
                if (Random.Range(0f, 100f) <= dropChance && goldPrefab != null)
                {
                    Instantiate(goldPrefab, transform.position, Quaternion.identity);
                }
            }

            Destroy(gameObject);
        }
    }
}