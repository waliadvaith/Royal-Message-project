using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public SpriteRenderer spriteRenderer;



    void Start()
    {
        // Get the Rigidbody2D component from the player object
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {


        // Get input in Update for responsiveness
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

    }

    void FixedUpdate()
    {
        if (movement.x > 0)
        {
            
        }
            if (movement.x < 0)
            {

            }
            if (movement.y > 0)
            {

            }
            if (movement.y < 0)
            {
            }
            // Apply movement to the Rigidbody in FixedUpdate (physics-safe)
            rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }
}
