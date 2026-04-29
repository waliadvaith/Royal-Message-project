using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5.0f;

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
    public float normalZoom = 5f;   // Standard zoom for Level 1
    public float castleZoom = 10f;  // Zoomed out for the Castle


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Tell Unity to call 'OnLevelFinishedLoading' every time a scene changes
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDestroy()
    {
        // Clean up the listener when the player is destroyed
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    // THIS IS THE FIX: This runs the millisecond the new scene is ready
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        // Find the camera attached to the player (or the Main Camera)
        Camera cam = GetComponentInChildren<Camera>();
        if (cam == null) cam = Camera.main;

        if (cam != null)
        {
            // Check if the scene name is the Castle one
            if (scene.name.Contains("Kingdom") || scene.name.Contains("Castle"))
            {
                cam.orthographicSize = castleZoom;
                useLimits = false; // Also remove those world boundaries
                Debug.Log("Camera Zoomed Out for Castle Scene");
            }
            else
            {
                cam.orthographicSize = normalZoom;
                useLimits = true;
            }
        }
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        UpdateCharacterSprite();
    }

    void UpdateCharacterSprite()
    {
        if (movement.magnitude > 0)
        {
            if (movement.y > 0)
            {
                characterSR.sprite = backSprite;
                if (hotbar != null) hotbar.UpdateWeaponVisuals("Up", false);
            }
            else if (movement.y < 0)
            {
                characterSR.sprite = frontSprite;
                if (hotbar != null) hotbar.UpdateWeaponVisuals("Down", false);
            }
            else if (movement.x != 0)
            {
                characterSR.sprite = sideSprite;
                bool isLeft = movement.x < 0;
                characterSR.flipX = isLeft;
                if (hotbar != null) hotbar.UpdateWeaponVisuals("Side", isLeft);
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 targetPosition = rb.position + movement.normalized * speed * Time.fixedDeltaTime;

        if (useLimits)
        {
            targetPosition.y = Mathf.Clamp(targetPosition.y, yMin, yMax);
            targetPosition.x = Mathf.Max(targetPosition.x, xMax);
        }

        rb.MovePosition(targetPosition);
    }
}