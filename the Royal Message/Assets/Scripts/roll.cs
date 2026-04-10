using System;
using System.Collections;
using UnityEngine;

public class Roll : MonoBehaviour
{
    public bool isInvincible = false;
    public float rollCooldown = 2f;
    private float nextRollTime = 0f;

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
    }

    // 3. Added missing Roll function
    void PerformRoll()
    {
        Debug.Log("Rolling!");
        // Add actual movement code here (e.g., rb.velocity = ...)
    }
}


