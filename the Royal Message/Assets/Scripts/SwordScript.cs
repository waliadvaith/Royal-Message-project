using UnityEngine;
using System.Collections.Generic;

public class SwordScript : MonoBehaviour
{
    [Header("References")]
    public Animator swordAnimator;

    [Header("Combat Settings")]
    public float damagePerHit = 25f;
    public float attackCooldown = 1.0f;
    [Tooltip("Set to 'Enemy' for Player, 'Player' for Enemies")]
    public string targetTag = "Player";

    [Header("Positioning & Flipping")]
    public float sideOffset = 0.5f;

    private bool isAttacking = false;
    private float nextAttackTime = 0f;
    private List<Collider2D> hitList = new List<Collider2D>();
    private Transform targetTransform;

    void Start()
    {
        if (swordAnimator == null) swordAnimator = GetComponent<Animator>();

        // Only look for a target if this is an Enemy sword
        if (transform.root.CompareTag("Enemy"))
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) targetTransform = p.transform;
        }
    }

    void Update()
    {
        // 1. POSITION & FLIP (ONLY for Enemies)
        // This ensures the Player's sword stays exactly where you put it manually
        if (transform.root.CompareTag("Enemy") && targetTransform != null)
        {
            UpdateEnemySwordOrientation();
        }

        // 2. PLAYER INPUT
        if (transform.root.CompareTag("Player") && Input.GetKeyDown(KeyCode.Space))
        {
            TryAttack();
        }
    }

    void UpdateEnemySwordOrientation()
    {
        float targetX = targetTransform.position.x;
        float myX = transform.root.position.x;

        // Swapped the rotation logic to fix the "reversing" issue
        if (targetX < myX)
        {
            // PLAYER IS ON LEFT
            transform.localPosition = new Vector3(-sideOffset, transform.localPosition.y, 0);
            transform.localRotation = Quaternion.Euler(0, 0, -90f); // Normal rotation
        }
        else
        {
            // PLAYER IS ON RIGHT
            transform.localPosition = new Vector3(sideOffset, transform.localPosition.y, 0);
            transform.localRotation = Quaternion.Euler(0, 180, -90f); // Flipped rotation
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

    public void StartAttack() { isAttacking = true; hitList.Clear(); }
    public void EndAttack() { isAttacking = false; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isAttacking && other.CompareTag(targetTag) && !hitList.Contains(other))
        {
            Health victimHealth = other.GetComponent<Health>() ?? other.GetComponentInParent<Health>();
            if (victimHealth != null)
            {
                victimHealth.TakeDamage(damagePerHit);
                hitList.Add(other);
            }
        }
    }
}