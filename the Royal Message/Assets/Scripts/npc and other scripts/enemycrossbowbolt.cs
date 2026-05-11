using UnityEngine;

public class enemycrosbowbolt : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    public float lifeSpan = 3f;

    void Start()
    {
        Destroy(gameObject, lifeSpan); // Cleanup
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health>()?.TakeDamage(damage);
            Debug.Log("Player hit by bolt!");
            Destroy(gameObject);
        }

    }
}