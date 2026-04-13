using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    float health;
    private Rigidbody2D rb;
    public EnemyBehavior movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
