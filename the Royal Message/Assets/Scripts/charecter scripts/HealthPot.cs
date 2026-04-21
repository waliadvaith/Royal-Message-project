using UnityEngine;

public class HealthPot : MonoBehaviour
{
    public float healAmount = 25f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the thing hitting the pot is the Player
        if (other.CompareTag("Player"))
        {
            Health playerHP = other.GetComponent<Health>();

            if (playerHP != null)
            {
                // Heal the player using your negative damage trick
                playerHP.TakeDamage(-healAmount);

                // Destroy the pot so it can't be used twice
                Destroy(gameObject);
            }
        }
    }
    void Update()
    {

    }
}