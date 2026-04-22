using UnityEngine;

public class BoltScript : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 20f;
    public float lifeTime = 2f;

    void Start()
    {
        // Destroy itself after a few seconds so the game doesn't lag
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Move forward based on where the crossbow was pointing
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Health enemyHP = other.GetComponent<Health>();
            if (enemyHP != null)
            {
                enemyHP.TakeDamage(damage);
            }
            Destroy(gameObject); // Poof!
        }
    }
}