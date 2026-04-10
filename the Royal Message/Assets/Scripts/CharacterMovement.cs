using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public float rotationSpeed = 720f;

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
        // Apply movement to the Rigidbody in FixedUpdate (physics-safe)
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }
}
