using UnityEngine;
using System.Collections.Generic;

public class SwordScript : MonoBehaviour
{
    [Header("References")]
    public Animator swordAnimator;

    [Header("Combat Settings")]
    public float damagePerHit = 25f;
    public float attackCooldown = 1.0f;
    public string targetTag = "Enemy"; // Set to 'Enemy' for Player, 'Player' for Enemies

    [Header("Positioning & Flipping")]
    public float sideOffset = 0.5f;

    private bool isAttacking = false;
    private float nextAttackTime = 0f;
    private List<Collider2D> hitList = new List<Collider2D>();
    private Transform targetTransform;

    void Start()
    {
        if (swordAnimator == null) swordAnimator = GetComponent<Animator>();

        // Only Enemies need to find a target to look at
        if (transform.root.CompareTag("Enemy"))
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) targetTransform = p.transform;
        }
    }

    void Update()
    {
        // 1. ORIENTATION (Only for Enemies)
        if (transform.root.CompareTag("Enemy") && targetTransform != null)
        {
            UpdateEnemySwordOrientation();
        }

        // 2. INPUT (Only for Player)
        // This only runs when the Hotbar has this object "Active"
        if (transform.root.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                TryAttack();
            }
        }
    }

    void UpdateEnemySwordOrientation()
    {
        if (targetTransform == null) return;

        float targetX = targetTransform.position.x;
        float myX = transform.root.position.x;

        if (targetX < myX) // Player is Left
        {
            transform.localPosition = new Vector3(-sideOffset, transform.localPosition.y, 0);
            transform.localRotation = Quaternion.Euler(0, 0, -90f);
        }
        else // Player is Right
        {
            transform.localPosition = new Vector3(sideOffset, transform.localPosition.y, 0);
            transform.localRotation = Quaternion.Euler(0, 180, -90f);
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

    // Reset attacking state if the weapon is swapped mid-animation
    void OnDisable()
    {
        isAttacking = false;
        hitList.Clear();
    }

    // --- Animation Events ---
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