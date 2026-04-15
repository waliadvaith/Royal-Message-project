using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5.0f;

    [Header("Movement Limits")]
    public float yMin = -2.0f; 
    public float yMax = 0.5f;
    public float xMax = -10f;

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

        
    }

    void FixedUpdate()
    {
        
        Vector2 targetPosition = rb.position + movement.normalized * speed * Time.fixedDeltaTime;

        
        targetPosition.y = Mathf.Clamp(targetPosition.y, yMin, yMax);
        targetPosition.x = Mathf.Max(targetPosition.x, xMax);
        
        rb.MovePosition(targetPosition);
    }
}