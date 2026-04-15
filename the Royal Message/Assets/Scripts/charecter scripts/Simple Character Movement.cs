
using System.Diagnostics.Contracts;
using UnityEngine;

public class SimpleCharacterMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5.0f;
    private Vector3 movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(movement.normalized * speed * Time.fixedDeltaTime);
    }

}