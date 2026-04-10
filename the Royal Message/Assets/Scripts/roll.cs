using System;
using System.Collections;
using UnityEngine;

public class roll : MonoBehaviour
{
    public bool isInvincible = false;
    public float rollCooldown = 2f;
    private float nextRollTime = 0f;
    public IEnumerable BecomeInvincible()
    {
        isInvincible = true;
        // Optional: Add flashing/transparency code here to show invincibility
        yield return new WaitForSeconds(1.0f); // Wait for 1 second
        isInvincible = false;
    }

    void Update()
    {
        // Check for input and if cooldown has elapsed
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= nextRollTime)
        {
            Roll();
            StartCoroutine (BecomeInvincible);
            // Reset next available time
            nextRollTime = Time.time + rollCooldown;
        }
    }

    void Roll()
    {
        Debug.Log("Rolling!");
        // Add your roll physics/animation here
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !isInvincible)
        {
            // Take Damage
        }
    }
}

