using UnityEngine;
using System.Collections;

public class CrossbowScript : MonoBehaviour
{
    public enum WeaponState { Empty, Reloading, Loaded }
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

    void Update()
    {
        if (!transform.root.CompareTag("Player")) return;

        AimAtMouse();

        if (currentState == WeaponState.Empty && !isWaitingToReload)
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

        // Ensure bool is false so we stay in Idle
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
        currentState = WeaponState.Empty;

        // 1. THE FIX: Force the Animator to the "Idle" state IMMEDIATELY
        // Replace "Idle" with the exact name of your empty/single-frame state
        crossbowAnim.Play("Idle", 0, 0f);

        // 2. Clear parameters
        crossbowAnim.SetBool("isReloading", false);
        crossbowAnim.SetTrigger("isFiring");

        // 3. Spawn bolt
        if (boltPrefab != null && firePoint != null)
        {
            Instantiate(boltPrefab, firePoint.position, transform.rotation * Quaternion.Euler(0, 0, 180));
        }
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
}