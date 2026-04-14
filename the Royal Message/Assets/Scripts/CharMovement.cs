using TMPro;
using UnityEngine;

public class CharMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector2 movement;
    private Rigidbody2D rb;
    public float speed = 5.0f;
    void Start()
    {
        movement.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        movement.y = Input.GetAxisRaw("Vertical")*Time.deltaTime*speed;
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(movement);
    }
}
