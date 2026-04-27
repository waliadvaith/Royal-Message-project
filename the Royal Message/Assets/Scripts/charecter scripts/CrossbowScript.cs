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

        if (currentState == WeaponState.Empty && !isWaitingToReload && currentAmmo > 0)
        {
            StartCoroutine(WaitAndReload());
        }
        else if (currentState == WeaponState.Empty && currentAmmo <= 0)
        {
            if (crossbowAnim != null)
            {
                crossbowAnim.SetBool("isReloading", false);
            }
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
        currentAmmo--;
        currentState = WeaponState.Empty;

        crossbowAnim.Play("Idle", 0, 0f);
        crossbowAnim.SetTrigger("isFiring");

        if (currentAmmo <= 0)
        {
            crossbowAnim.SetBool("isReloading", false);
        }

        if (boltPrefab != null && firePoint != null)
        {
            Instantiate(boltPrefab, firePoint.position, transform.rotation * Quaternion.Euler(0, 0, 180));
        }
    }

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);
        Debug.Log("Ammo added! Total: " + currentAmmo);
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