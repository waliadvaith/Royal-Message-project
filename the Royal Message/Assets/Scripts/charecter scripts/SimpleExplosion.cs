using UnityEngine;

public class SimpleExplosion : MonoBehaviour
{
    public float lifeTime = 0.2f; // How long it stays (0.2 is a "split second")
    public float growSpeed = 15f; // How fast it expands

    void Start()
    {
        // This is the magic line that deletes it automatically
        Destroy(gameObject, lifeTime);

        // Start at size 0
        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        // Make the circle expand every frame
        transform.localScale += Vector3.one * growSpeed * Time.deltaTime;
    }
}