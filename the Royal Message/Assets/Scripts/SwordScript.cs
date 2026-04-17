using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public Animator swordAnimator;
    public float damagePerHit = 25f;
    private bool isAttacking = false; // The gatekeeper

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        isAttacking = true;
        swordAnimator.SetTrigger("isAttack");

        // Reset the attack after 0.5 seconds (or however long your swing is)
        Invoke("EndAttack", 0.5f);
    }

    void EndAttack()
    {
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Now it only works if isAttacking is TRUE
        if (isAttacking && other.CompareTag("Enemy"))
        {
            Health victimHealth = other.GetComponentInParent<Health>();
            if (victimHealth != null)
            {
                victimHealth.TakeDamage(damagePerHit);
            }
        }
    }
}