using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Splines;
using System;
using UnityEditor;


public class SwordAttack : MonoBehaviour
{
    public float attackCooldown = 1.0f; // Seconds between attacks
    private float nextAttackTime = 0f;

    void Update()
    {
        // Check if the current time has passed the 'nextAttackTime'
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0)) // Left Click
            {
                Attack();
                // Update the cooldown timestamp
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void Attack()
    {
        Debug.Log("Sword Swung!");
        // Add your animation or damage logic here
    }
}
