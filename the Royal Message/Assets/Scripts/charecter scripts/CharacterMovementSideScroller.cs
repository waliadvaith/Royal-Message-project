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

    void UpdateCharacterSpriteAndAnimation()
    {
        if (movement.sqrMagnitude > 0.01f) // If moving
        {
            if (movement.y > 0) // UP
            {
                characterSR.sprite = backSprite;
                if (anim != null) anim.SetInteger("direction", 1);
                if (hotbar != null) hotbar.UpdateWeaponVisuals("Up", false);
            }
            else if (movement.y < 0) // DOWN
            {
                characterSR.sprite = frontSprite;
                if (anim != null) anim.SetInteger("direction", 0);
                if (hotbar != null) hotbar.UpdateWeaponVisuals("Down", false);
            }
            else if (movement.x > 0) // RIGHT
            {
                characterSR.sprite = sideSprite;
                
                if (anim != null) anim.SetInteger("direction", 3);
                if (hotbar != null) hotbar.UpdateWeaponVisuals("Side", false);
            }
            else if (movement.x < 0) // LEFT
            {
                characterSR.sprite = sideSprite;
                
                if (anim != null) anim.SetInteger("direction", 2);
                if (hotbar != null) hotbar.UpdateWeaponVisuals("Side", true);
            }
        }
        else // IDLE - Not moving
        {
            // Reset to Forward (Front) sprite and animation
            characterSR.sprite = frontSprite;
            
            // Optional: reset flipX if you want him always facing same way when idle
            characterSR.flipX = false;
        }
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