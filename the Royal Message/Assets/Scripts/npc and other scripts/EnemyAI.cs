using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 1.2f;
    public float detectionRange = 7.0f; // NEW: How close the player needs to be to "wake up" the enemy

    private Transform playerTransform;
    private Rigidbody2D rb;
    private SwordScript mySword;
    private bool isChasing = false; // Tracks if the enemy has seen the player
    private bool isFleeing = false;
    public float fleeSpeedMultiplier = 1.5f;
    public List<DialogueLine> FleeingDialogue;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mySword = GetComponentInChildren<SwordScript>();

        // We find the player, but we won't act until they are in range
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) playerTransform = p.transform;
    }

    void FixedUpdate()
    {
        if (playerTransform == null) return;
        float distance = Vector2.Distance(transform.position, playerTransform.position);

        if (isFleeing)
        {
            // Run AWAY from player
            Vector2 dirAway = ((Vector2)rb.position - (Vector2)playerTransform.position).normalized;
            rb.MovePosition(rb.position + dirAway * (speed * fleeSpeedMultiplier) * Time.fixedDeltaTime);

            // MERCY CHECK: If they get away, give morality
            if (distance > 15f)
            {
                Object.FindFirstObjectByType<MoralityManager>()?.AddMorality(2);
                Destroy(gameObject);
            }
            return;
        }

        // --- DETECTION LOGIC ---
        if (!isChasing)
        {
            if (distance <= detectionRange)
            {
                isChasing = true;
                Debug.Log(gameObject.name + " spotted the player!");
            }
            else
            {
                return; // Stay still if player is too far away
            }
        }

        // --- MOVEMENT LOGIC (Only runs if isChasing is true) ---
        if (distance > attackRange * 0.9f)
        {
            Vector2 dir = ((Vector2)playerTransform.position - rb.position).normalized;
            rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
        }

        // --- SWORD LOGIC ---
        if (mySword != null)
        {
            // Face the player
            float side = (playerTransform.position.x < transform.position.x) ? -0.5f : 0.5f;
            float yRot = (playerTransform.position.x < transform.position.x) ? 0 : 180;
            mySword.transform.localPosition = new Vector3(side, 0, 0);
            mySword.transform.localRotation = Quaternion.Euler(0, yRot, -90f);

            // Attack if in range
            if (distance <= attackRange)
            {
                mySword.TryAttack();
            }
        }
    }

    // Helper: Draws a blue circle in the editor so you can see the detection range
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    public void StartFleeing()
    {
        isFleeing = true;
        isChasing = false;
        if (mySword != null) mySword.gameObject.SetActive(false); // Drop weapon

        // TRIGGER DIALOGUE HERE
        if (DialogueManager.instance != null && FleeingDialogue.Count > 0)
        {
            DialogueManager.instance.StartDialogue(FleeingDialogue);
        }
    }
}