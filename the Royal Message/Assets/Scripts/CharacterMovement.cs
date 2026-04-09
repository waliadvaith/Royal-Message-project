using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 5.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxisRaw("Horizontal")*Time.deltaTime*speed, Input.GetAxisRaw("Vertical")*Time.deltaTime*speed, 0);
    }
}