using UnityEngine;

public class CrossbowEnemyVisuals : MonoBehaviour
{
    [Header("Setup")]
    public Transform player;
    public Transform weaponSpriteTransform; // Drag the Crossbow Sprite child here
    public GameObject boltPrefab;
    public Transform firePoint;

    [Header("Visual Settings")]
    public float rotationOffset = 0f;
    public bool flipYWhenLeft = true; // Prevents the crossbow from looking upside down

    [Header("Stats")]
    public float range = 8f;
    public float fireRate = 2f;
    private float nextFireTime;

    void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= range)
        {
            AimVisuals();

            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void AimVisuals()
    {
        if (weaponSpriteTransform == null) return;

        // Calculate direction
        Vector3 direction = player.position - weaponSpriteTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply rotation to the VISUAL only
        weaponSpriteTransform.rotation = Quaternion.Euler(0, 0, angle + rotationOffset);

        // Optional: Fix "Upside Down" look when pointing left
        if (flipYWhenLeft)
        {
            Vector3 localScale = Vector3.one;
            // If the angle is pointing left, flip the Y scale of the sprite
            if (angle > 90 || angle < -90)
            {
                localScale.y = -1f;
            }
            else
            {
                localScale.y = 1f;
            }
            weaponSpriteTransform.localScale = localScale;
        }
    }

    void Shoot()
    {
        // Spawns the bolt based on the visual rotation of the firepoint
        Instantiate(boltPrefab, firePoint.position, firePoint.rotation);
    }
}