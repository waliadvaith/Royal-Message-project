using UnityEngine;

public class movethisway : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Animator animator;
    Vector2 movement;

    void Update()
    {
        // Get input (e.g., WASD or Arrow keys)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Pass values to the Animator
        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveY", movement.y);

    }
}
