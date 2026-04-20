using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
<<<<<<< Updated upstream:the Royal Message/Assets/Scripts/npc and other scripts/EnemyBehavior.cs
    public float health;
    private Rigidbody2D rb;
    public EnemyBehavior movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
=======
    public float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 movement;
<<<<<<< HEAD
=======
    public float rotationSpeed = 720f;
>>>>>>> 2eeb6e511612f5879bcf363794b82e575d9d4502

    void Start()
    {
        // Get the Rigidbody2D component from the player object
>>>>>>> Stashed changes:the Royal Message/Assets/Scripts/CharacterMovement.cs
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
<<<<<<< Updated upstream:the Royal Message/Assets/Scripts/npc and other scripts/EnemyBehavior.cs
        
    }
=======
<<<<<<< HEAD
        transform.Translate(Input.GetAxisRaw("Horizontal")*Time.deltaTime*speed, Input.GetAxisRaw("Vertical")*Time.deltaTime*speed, 0);

        // Get input in Update for responsiveness
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Get input in Update for responsiveness
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
=======
        
    
    // Get input in Update for responsiveness
    movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

>>>>>>> 2eeb6e511612f5879bcf363794b82e575d9d4502
    }

    void FixedUpdate()
    {
        // Apply movement to the Rigidbody in FixedUpdate (physics-safe)
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }
>>>>>>> Stashed changes:the Royal Message/Assets/Scripts/CharacterMovement.cs
}
