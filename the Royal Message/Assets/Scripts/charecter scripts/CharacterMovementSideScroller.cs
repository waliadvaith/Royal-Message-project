using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5.0f;
    

    [Header("Animations")]
    public Animator anim;

    [Header("Movement Limits")]
    public bool useLimits = true;
    public float yMin = -2.0f;
    public float yMax = 0.5f;
    public float xMax = -10f;

    [Header("Visuals")]
    public SpriteRenderer characterSR;
    public Sprite frontSprite;
    public Sprite backSprite;
    public Sprite sideSprite;

    private Rigidbody2D rb;
    private Vector2 movement;
    public HotbarManager hotbar;
    public SwordScript sword;

    [Header("Camera Zoom Settings")]
    public float normalZoom = 5f;
    public float castleZoom = 10f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Ensure Rigidbody is set to Kinematic or has 0 Linear Drag if you want it super snappy
        rb.gravityScale = 0;
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void Update()
    {
        // GetAxisRaw is "snappy" - it goes 0 to 1 instantly with no sliding
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        UpdateCharacterSpriteAndAnimation();
    }

    // 1. Add a variable to track the current direction
    private int lastDirection = -1;

    // Add this at the top of your class with your other variables



    void UpdateCharacterSpriteAndAnimation()
    {
        int currentDirection = lastDirection;

        // 0.1f threshold to kill "ghost" inputs when letting go of keys
        if (movement.sqrMagnitude > 0.1f)
        {
            if (Mathf.Abs(movement.y) >= Mathf.Abs(movement.x))
            {
                currentDirection = (movement.y > 0) ? 1 : 0; // 1: Back, 0: Front
            }
            else
            {
                currentDirection = (movement.x > 0) ? 3 : 2; // 2: Right, 3: Left
            }
        }
        else
        {
            currentDirection = 5; // Idle
            rb.linearVelocity = Vector2.zero;
        }

        // Only trigger if the state actually changes
        if (currentDirection != lastDirection)
        {
            if (anim != null)
            {
                anim.SetInteger("direction", currentDirection);
                // Force the Animator to the new state immediately
                anim.Update(0);
            }

            // Sync the static sprite renderer to the correct starting frame
            UpdateSpriteVisuals(currentDirection);

            lastDirection = currentDirection;
        }
        // Inside UpdateCharacterSpriteAndAnimation() in CharacterMovement.cs

        if (currentDirection == 1) // Backwards
        {
            if (sword != null) sword.SetDirection("Up");
        }
        else if (currentDirection == 0 || currentDirection == 5) // Forward/Idle
        {
            if (sword != null) sword.SetDirection("Down");
        }
        else if (currentDirection == 2) // Right
        {
            if (sword != null) sword.SetDirection("Right");
        }
        else if (currentDirection == 3) // Left
        {
            if (sword != null) sword.SetDirection("Left");
        }
    }

    void UpdateSpriteVisuals(int dir)
    {
        // NO flipX here—let the animation handle the orientation
        if (dir == 0 || dir == 5) characterSR.sprite = frontSprite;
        else if (dir == 1) characterSR.sprite = backSprite;
        else if (dir == 2 || dir == 3) characterSR.sprite = sideSprite;

        // Safety: Ensure flipX is ALWAYS false so it doesn't mess with the anims
        characterSR.flipX = false;
    }


    void FixedUpdate()
    {
        // 1. Calculate the movement normally
        Vector2 targetPosition = rb.position + movement.normalized * speed * Time.fixedDeltaTime;

        // 2. ONLY apply the limits if we are NOT in a "Jonah" scene
        if (useLimits)
        {
            // Y-Axis Clamp (Vertical Road)
            targetPosition.y = Mathf.Clamp(targetPosition.y, yMin, yMax);

            // X-Axis Limit (Stop them from going too far left)
            targetPosition.x = Mathf.Max(targetPosition.x, xMax);
        }

        // 3. Move the player
        rb.MovePosition(targetPosition);
    }

    // Camera logic remains the same...
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        // 1. Find the Camera
        Camera cam = GetComponentInChildren<Camera>();
        if (cam == null) cam = Camera.main;

        // 2. CHECK SCENE NAME
        // We check for "jonah" (case-insensitive check is safer)
        if (scene.name.ToLower().Contains("jonah"))
        {
            // REMOVE CLAMPS for the designer's levels
            useLimits = false;

            // Set the wide-view zoom
            if (cam != null) cam.orthographicSize = castleZoom;

            Debug.Log("Entered Designer Scene (" + scene.name + "): Limits Removed.");
        }
        else
        {
            // ADD CLAMPS BACK for your generated road/standard levels
            useLimits = true;

            // Return to normal zoom
            if (cam != null) cam.orthographicSize = normalZoom;

            Debug.Log("Entered Standard Path: Limits Applied.");
        }
    }
}