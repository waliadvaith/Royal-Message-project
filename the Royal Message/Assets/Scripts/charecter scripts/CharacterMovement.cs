using Unity.Mathematics;
using UnityEngine;

public class CharacterMovementSideScroller : MonoBehaviour
{
    public float speed = 5.0f;
    public float switchRate = 0.5f; // Seconds between frames
    private float nextSwitch;

    [Header("Movement Limits")]
    public float yMin = -2.0f;
    public float yMax = 0.5f;

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

        {
            if (Input.GetKey(KeyCode.RightArrow) && Time.time > nextSwitch)
            {
                nextSwitch = Time.time + switchRate;
            }
        }

    }

    void FixedUpdate()
    {

        Vector2 targetPosition = rb.position + movement.normalized * speed * Time.fixedDeltaTime;


        targetPosition.y = Mathf.Clamp(targetPosition.y, yMin, yMax);


        rb.MovePosition(targetPosition);




    }
}