using UnityEngine;

public class CrossbowScript : MonoBehaviour
{
    [Header("Settings")]
    public GameObject boltPrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;

    private float nextFireTime = 0f;

    void Update()
    {
        // Only the Player aims and shoots
        if (transform.root.CompareTag("Player"))
        {
            AimAtMouse();

            if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void AimAtMouse()
    {
        // 1. Get Mouse Position in World Space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 2. Calculate Direction
        Vector2 direction = (Vector2)(mousePos - transform.position);

        // 3. Calculate Angle in Degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 4. Apply Rotation
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // 5. Flip Fix (Optional)
        // If the crossbow is pointing left, it will be upside down. 
        // This flips the Y scale to keep it looking right-side up.
        Vector3 localScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = 1f;
        }
        transform.localScale = localScale;
    }

    void Shoot()
    {
        if (boltPrefab != null && firePoint != null)
        {
            // Spawn the bolt using the current rotation of the crossbow/firePoint
            Instantiate(boltPrefab, firePoint.position, firePoint.rotation);
        }
    }
}