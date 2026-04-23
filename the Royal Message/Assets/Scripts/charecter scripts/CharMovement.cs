using UnityEngine;

public class CharMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5.0f;
    private Vector2 movement;
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
       
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }
}