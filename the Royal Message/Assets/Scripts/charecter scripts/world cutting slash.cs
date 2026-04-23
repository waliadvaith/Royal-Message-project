using UnityEngine;

public class worldcuttingslash : MonoBehaviour
{
    

public class BoltScript : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 20f;
    public float lifeTime = 2f;

    void Start()
    {
        
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
}
