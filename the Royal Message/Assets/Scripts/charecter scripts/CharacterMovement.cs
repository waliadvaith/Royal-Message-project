using UnityEngine;

public class CharacterMovementFreeMovement : MonoBehaviour
{
    public float speed = 5.0f;

    [Header("Visuals")]
    public SpriteRenderer characterSR; // Drag your SpriteRenderer here
    public Sprite frontSprite;         // Facing down
    public Sprite backSprite;          // Facing up
    public Sprite sideSprite;          // Facing left/right

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        UpdateCharacterSprite();
    }

    void UpdateCharacterSprite()
    {
        // Only change sprite if the player is actually moving
        if (movement.magnitude > 0)
        {
            // Prioritize Vertical sprites (Up/Down)
            if (movement.y > 0)
            {
                characterSR.sprite = backSprite;
            }
            else if (movement.y < 0)
            {
                characterSR.sprite = frontSprite;
            }
            // If not moving up/down, check horizontal
            else if (movement.x != 0)
            {
                characterSR.sprite = sideSprite;

                // Flip the side sprite based on direction (Left = flipped, Right = normal)
                characterSR.flipX = (movement.x < 0);
            }
        }
    }

    void FixedUpdate()
    {
        // Simplified movement without any position clamping
        Vector2 targetPosition = rb.position + movement.normalized * speed * Time.fixedDeltaTime;

        rb.MovePosition(targetPosition);
    }
}