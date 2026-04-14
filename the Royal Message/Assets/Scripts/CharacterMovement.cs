using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5.0f;

    [Header("Movement Limits")]
    public float yMin = -2.0f; // Bottom of the path
    public float yMax = 0.5f;  // Top of the path (near the trees)

    private Rigidbody2D rb;
    private Vector2 movement;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Flip the sprite based on direction
        if (movement.x > 0) spriteRenderer.flipX = false;
        else if (movement.x < 0) spriteRenderer.flipX = true;
    }

    void FixedUpdate()
    {
        // 1. Calculate where the player wants to go
        Vector2 targetPosition = rb.position + movement.normalized * speed * Time.fixedDeltaTime;

        // 2. Clamp the Y value so they stay within the top and bottom of the path
        targetPosition.y = Mathf.Clamp(targetPosition.y, yMin, yMax);

        // 3. Move the Rigidbody
        rb.MovePosition(targetPosition);
    }
}