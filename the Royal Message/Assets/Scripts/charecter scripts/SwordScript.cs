using UnityEngine;
using System.Collections.Generic;

public class SwordScript : MonoBehaviour
{
    [Header("Combat Settings")]
    public float damagePerHit = 25f;
    public float attackCooldown = 1.0f;
    public string targetTag = "Enemy"; // Set to "Player" for Barbarians

    public Animator swordAnimator;
    private bool isAttacking = false;
    private float nextAttackTime = 0f;
    private List<Collider2D> hitList = new List<Collider2D>();
    private bool isPlayer;

    void Start()
    {
        if (swordAnimator == null) swordAnimator = GetComponent<Animator>();
        isPlayer = transform.root.CompareTag("Player");
    }

    void Update()
    {
        // Only the player uses keyboard input
        if (isPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                TryAttack();
            }
        }
    }

    public void TryAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            if (swordAnimator != null)
            {
                swordAnimator.SetTrigger("isAttack");
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    // IMPORTANT: Make sure these are called by Animation Events in your Attack Clip!
    public void StartAttack() { isAttacking = true; hitList.Clear(); }
    public void EndAttack() { isAttacking = false; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isAttacking && other.CompareTag(targetTag) && !hitList.Contains(other))
        {
            Health v = other.GetComponent<Health>() ?? other.GetComponentInParent<Health>();
            if (v != null)
            {
                v.TakeDamage(damagePerHit);
                hitList.Add(other);
            }
        }
    }
}