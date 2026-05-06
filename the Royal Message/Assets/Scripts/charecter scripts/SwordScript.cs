using UnityEngine;
using System.Collections.Generic;

public class SwordScript : MonoBehaviour
{
    [Header("Combat Settings")]
    public float damagePerHit = 25f;
    public float attackCooldown = 1.0f;
    public string targetTag = "Enemy";

    [Header("Positioning")]
    // Adjust X in Inspector. Example: 0.5 for Right, -0.5 for Left
    public float sideOffset = 0.5f;

    public Animator swordAnimator;
    private bool isAttacking = false;
    private float nextAttackTime = 0f;
    private List<Collider2D> hitList = new List<Collider2D>();
    private bool isPlayer;
    public HotbarManager hotbar;

    private Vector3 originalScale;

    void Start()
    {
        if (swordAnimator == null) swordAnimator = GetComponent<Animator>();
        isPlayer = transform.root.CompareTag("Player");
        hotbar = GetComponentInParent<HotbarManager>();

        // SAVE the size you set in the Inspector so it doesn't get tiny
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (isPlayer)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && hotbar.canAttack)
            {
                TryAttack();
            }
        }
    }

    // Only flips and offsets when moving sideways
    // Replace your SetDirection function in SwordScript with this:
    public void SetDirection(string dir)
    {
        SpriteRenderer swordSR = GetComponent<SpriteRenderer>();

        if (dir == "Right")
        {
            // Move to the right side
            transform.localPosition = new Vector3(sideOffset, transform.localPosition.y, 0);

            // Ensure sprite is NOT flipped
            if (swordSR != null) swordSR.flipY = false;

            // Reset scale to your inspector size just in case
            transform.localScale = originalScale;
        }
        else if (dir == "Left")
        {
            // Move to the left side
            transform.localPosition = new Vector3(-sideOffset, transform.localPosition.y, 0);

            // Flip the SPRITE, not the whole object (prevents the upside-down glitch)
            if (swordSR != null) swordSR.flipY = true;

            // Reset scale to your inspector size
            transform.localScale = originalScale;
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
            Health v = other.GetComponent<Health>() ?? other.GetComponentInParent<Health>();
            if (v != null)
            {
                v.TakeDamage(damagePerHit);
                hitList.Add(other);
            }
        }
    }
}