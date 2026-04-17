using UnityEngine;
using System.Collections.Generic; // Added for the hit list

public class SwordScript : MonoBehaviour
{
    public Animator swordAnimator;
    public float damagePerHit = 25f;

    [Header("State Control")]
    public bool isAttacking = false; // This is our gate
    private List<Collider2D> hitList = new List<Collider2D>(); // Prevents double-hitting

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            swordAnimator.SetTrigger("isAttack");
        }
    }

    // --- CALL THESE VIA ANIMATION EVENTS ---
    public void StartAttack()
    {
        isAttacking = true;
        hitList.Clear(); // Reset so we can hit things again this swing
    }

    public void EndAttack()
    {
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Only proceed if the gate is OPEN (isAttacking is true)
        if (!isAttacking) return;

        // 2. Look for Health script
        Health victimHealth = other.GetComponentInParent<Health>();

        // 3. Check tag and ensure we haven't hit this specific enemy in this swing yet
        if (victimHealth != null && other.CompareTag("Enemy") && !hitList.Contains(other))
        {
            victimHealth.TakeDamage(damagePerHit);
            hitList.Add(other); // Add to list so they don't get hit twice
            Debug.Log("Dealt " + damagePerHit + " damage to " + other.name);
        }
    }
}