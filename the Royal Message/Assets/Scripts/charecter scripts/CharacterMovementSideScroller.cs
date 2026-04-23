using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5.0f;

    [Header("Movement Limits")]
    public float yMin = -2.0f;
    public float yMax = 0.5f;
    public float xMax = -10f;

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
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        UpdateCharacterSprite();
    }

    public HotbarManager hotbar; // Drag your Hotbar script here

    void UpdateCharacterSprite()
    {
        if (movement.magnitude > 0)
        {
            if (movement.y > 0)
            {
                characterSR.sprite = backSprite;
                hotbar.UpdateWeaponVisuals("Up", false);
            }
            else if (movement.y < 0)
            {
                characterSR.sprite = frontSprite;
                hotbar.UpdateWeaponVisuals("Down", false);
            }
            else if (movement.x != 0)
            {
                characterSR.sprite = sideSprite;
                bool isLeft = movement.x < 0;
                characterSR.flipX = isLeft;
                hotbar.UpdateWeaponVisuals("Side", isLeft);
            }
        }
    }


    void FixedUpdate()
    {
        Vector2 targetPosition = rb.position + movement.normalized * speed * Time.fixedDeltaTime;

        targetPosition.y = Mathf.Clamp(targetPosition.y, yMin, yMax);
        targetPosition.x = Mathf.Max(targetPosition.x, xMax);

        rb.MovePosition(targetPosition);
    }
}