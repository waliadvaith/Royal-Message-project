using System;
using System.Collections;
using UnityEngine;

public class roll : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D rb;
    public bool isInvincible = false;
    public float rollCooldown = 2f;
    private float nextRollTime = 0f;



    private Vector2 movement;
    private Vector2 velocity;
    void Start()
    {
        // Get the Rigidbody2D component from the player object
        rb = GetComponent<Rigidbody2D>();
    }

    // 1. Changed IEnumerable to IEnumerator
    public IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        Debug.Log("Invincible: True");
        // Optional: Add flashing/transparency code here
        yield return new WaitForSeconds(1.0f); // Wait for 1 second
        isInvincible = false;
        Debug.Log("Invincible: False");
    }

    void Update()
    {
        // Check for input and if cooldown has elapsed
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= nextRollTime)
        {
            PerformRoll(); // 2. Call the function
            StartCoroutine(BecomeInvincible());

            // Reset next available time
            nextRollTime = Time.time + rollCooldown;
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    // 3. Added missing Roll function
    void PerformRoll()
    {
        Debug.Log("Rolling!");


        // Add actual movement code here (e.g., rb.velocity = ...)


        {
            rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
        }

    }
}


