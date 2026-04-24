using UnityEngine;
using System.Collections;

public class CrossbowScript : MonoBehaviour
{
    public enum WeaponState { Empty, Reloading, Loaded }

    [Header("Ammo Settings")]
    public int currentAmmo = 10;
    public int maxAmmo = 30;

    [Header("Debug Info")]
    public WeaponState currentState = WeaponState.Empty;

    [Header("References")]
    public GameObject boltPrefab;
    public Transform firePoint;
    public Animator crossbowAnim;
    public float visualOffset = 0f;

    [Header("Timing")]
    public float emptyDelay = 0.2f;

    private bool isWaitingToReload = false;

    void OnEnable()
    {
        // Fix for the glitchy animation: Force the animator to reset when equipped
        if (crossbowAnim != null)
        {
            crossbowAnim.Rebind();
            crossbowAnim.Update(0f);
        }
    }

    void Update()
    {
        if (!transform.root.CompareTag("Player")) return;

        AimAtMouse();

        // Only reload if we actually HAVE ammo left
        if (currentState == WeaponState.Empty && !isWaitingToReload && currentAmmo > 0)
        {
            StartCoroutine(WaitAndReload());
        }
        else if (currentState == WeaponState.Reloading)
        {
            CheckReloadProgress();
        }
        else if (currentState == WeaponState.Loaded)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }
        }
    }

    IEnumerator WaitAndReload()
    {
        isWaitingToReload = true;
        crossbowAnim.SetBool("isReloading", false);

        yield return new WaitForSeconds(emptyDelay);

        currentState = WeaponState.Reloading;
        crossbowAnim.SetBool("isReloading", true);

        isWaitingToReload = false;
    }

    void CheckReloadProgress()
    {
        AnimatorStateInfo stateInfo = crossbowAnim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("crossbowreload"))
        {
            if (stateInfo.normalizedTime >= 0.95f && !crossbowAnim.IsInTransition(0))
            {
                currentState = WeaponState.Loaded;
            }
        }
    }

    void Fire()
    {
        // Spend one ammo
        currentAmmo--;

        currentState = WeaponState.Empty;
        crossbowAnim.Play("Idle", 0, 0f);
        crossbowAnim.SetBool("isReloading", false);
        crossbowAnim.SetTrigger("isFiring");

        if (boltPrefab != null && firePoint != null)
        {
            Instantiate(boltPrefab, firePoint.position, transform.rotation * Quaternion.Euler(0, 0, 180));
        }
    }

    // Call this from your Pickup Script!
    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo > maxAmmo) currentAmmo = maxAmmo;
        Debug.Log("Ammo Picked Up! Total: " + currentAmmo);
    }

    void AimAtMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (Vector2)(mousePos - transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + visualOffset);

        Vector3 s = transform.localScale;
        s.y = (angle > 90 || angle < -90) ? -1f : 1f;
        transform.localScale = s;
    }

    void OnDisable()
    {
        currentState = WeaponState.Empty;
        isWaitingToReload = false;
        StopAllCoroutines();

        if (crossbowAnim != null)
        {
            crossbowAnim.SetBool("isReloading", false);
        }
    }
}