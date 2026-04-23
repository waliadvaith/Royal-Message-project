using UnityEngine;
using System.Collections.Generic;

public class SwordScript : MonoBehaviour
{
    public Animator swordAnimator;
    public float damagePerHit = 25f;
    public float attackCooldown = 1.0f;
    public string targetTag = "Enemy";
    public float sideOffset = 0.5f;

    private bool isAttacking = false;
    private float nextAttackTime = 0f;
    private List<Collider2D> hitList = new List<Collider2D>();
    private Transform targetTransform;

    void Start()
    {
        if (swordAnimator == null) swordAnimator = GetComponent<Animator>();

        // Enemies still need to find targets
        if (transform.root.CompareTag("Enemy"))
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) targetTransform = p.transform;
        }
    }

    void Update()
    {
        // ENEMIES handle their own orientation
        if (transform.root.CompareTag("Enemy") && targetTransform != null)
        {
            UpdateEnemyOrientation();
        }

        // PLAYER just handles attack input
        if (transform.root.CompareTag("Player") && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            TryAttack();
        }
    }

    void UpdateEnemyOrientation()
    {
        float targetX = targetTransform.position.x;
        float myX = transform.root.position.x;

        if (targetX < myX)
        {
            transform.localPosition = new Vector3(-sideOffset, 0, 0);
            transform.localRotation = Quaternion.Euler(0, 0, -90f);
        }
        else
        {
            transform.localPosition = new Vector3(sideOffset, 0, 0);
            transform.localRotation = Quaternion.Euler(0, 180, -90f);
        }
    }

    public void TryAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            swordAnimator.SetTrigger("isAttack");
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void OnDisable() { isAttacking = false; }
    public void StartAttack() { isAttacking = true; hitList.Clear(); }
    public void EndAttack() { isAttacking = false; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isAttacking && other.CompareTag(targetTag) && !hitList.Contains(other))
        {
            Health v = other.GetComponent<Health>() ?? other.GetComponentInParent<Health>();
            if (v != null) { v.TakeDamage(damagePerHit); hitList.Add(other); }
        }
    }
}