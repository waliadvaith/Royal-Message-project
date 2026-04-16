using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public Animator swordAnimator;
    public float damagePerHit = 25f;

    void Update()
    {
        // Simple Spacebar swing for testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            swordAnimator.SetTrigger("isAttack");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Look for Health script on the object or its parent
        Health victimHealth = other.GetComponentInParent<Health>();

        // 2. If it exists and is an Enemy, deal damage
        if (victimHealth != null && other.CompareTag("Enemy"))
        {
            victimHealth.TakeDamage(damagePerHit);
            Debug.Log("Dealt " + damagePerHit + " damage to " + other.name);
        }
    }
}
