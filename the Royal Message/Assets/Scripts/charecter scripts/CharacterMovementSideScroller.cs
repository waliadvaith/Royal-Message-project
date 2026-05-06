using UnityEngine;
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
        int currentDirection = -1;

        if (movement.sqrMagnitude > 0.01f)
        {
            if (Mathf.Abs(movement.y) >= Mathf.Abs(movement.x))
            {
                currentDirection = (movement.y > 0) ? 1 : 0; // 1: Backwards, 0: Forward
            }
            else
            {
                currentDirection = (movement.x > 0) ? 3 : 2; // 2: Right, 3: Left
            }
        }
        else
        {
            currentDirection = 0; // Default to Forward/Idle
            rb.linearVelocity = Vector2.zero;
        }

        if (currentDirection != lastDirection)
        {
            if (anim != null)
            {
                anim.SetInteger("direction", currentDirection);
                // This forces the frame to swap immediately
                anim.Update(0);
            }

            // Set the static sprite as a fallback
            if (currentDirection == 0) characterSR.sprite = frontSprite;
            else if (currentDirection == 1) characterSR.sprite = backSprite;
            else if (currentDirection == 2) characterSR.sprite = sideSprite;
            else if (currentDirection == 3) characterSR.sprite = sideSprite;

            // REMOVED: characterSR.flipX logic
            lastDirection = currentDirection;
        }
    }



    // Helper to keep logic clean
    void UpdateSpriteVisuals(int dir)
    {
        if (dir == 0) characterSR.sprite = frontSprite;
        else if (dir == 1) characterSR.sprite = backSprite;
        else if (dir == 2) { characterSR.sprite = sideSprite; characterSR.flipX = false; }
        else if (dir == 3) { characterSR.sprite = sideSprite; characterSR.flipX = true; }
    }


    void FixedUpdate()
    {
        // Normalize movement so diagonal walking isn't faster
        Vector2 targetPosition = rb.position + movement.normalized * speed * Time.fixedDeltaTime;

        if (useLimits)
        {
            targetPosition.y = Mathf.Clamp(targetPosition.y, yMin, yMax);
            targetPosition.x = Mathf.Max(targetPosition.x, xMax);
        }

        // Use MovePosition for Rigidbody-based snappiness
        rb.MovePosition(targetPosition);
    }

    // Camera logic remains the same...
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Camera cam = GetComponentInChildren<Camera>();
        if (cam == null) cam = Camera.main;
        if (cam != null)
        {
            if (scene.name.Contains("Kingdom") || scene.name.Contains("Castle"))
            {
                cam.orthographicSize = castleZoom;
                useLimits = false;
            }
            else
            {
                cam.orthographicSize = normalZoom;
                useLimits = true;
            }
        }
    }
}