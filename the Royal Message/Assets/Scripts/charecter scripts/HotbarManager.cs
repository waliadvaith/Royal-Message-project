using UnityEngine;

public class HotbarManager : MonoBehaviour
{
    [Header("Weapon Slots")]
    public GameObject[] weapons;
    private int currentSlot = 0;

    [Header("Sorting & Offset")]
    public int playerBaseOrder = 10;
    public float sideOffset = 0.5f;
    public float verticalOffset = -0.2f;

    // Track the last horizontal side so the weapon doesn't "center" when moving purely Up/Down
    private bool lastWasLeft = false;

    void Start()
    {
        SelectSlot(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(1);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) SelectSlot((currentSlot + 1) % weapons.Length);
        else if (scroll < 0f) SelectSlot((currentSlot - 1 + weapons.Length) % weapons.Length);
    }

    public void SelectSlot(int index)
    {
        if (index < 0 || index >= weapons.Length) return;
        currentSlot = index;

        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] != null) weapons[i].SetActive(i == index);
        }
    }

    public void UpdateWeaponVisuals(string direction, bool isLeft)
    {
        GameObject currentWeapon = weapons[currentSlot];
        if (currentWeapon == null) return;

        SpriteRenderer weaponSR = currentWeapon.GetComponent<SpriteRenderer>();

        // 1. UPDATE THE SIDE TRACKER
        // This ensures the weapon stays on the left/right even if we are only moving Up or Down
        if (direction == "Side")
        {
            lastWasLeft = isLeft;
        }

        // 2. ALWAYS USE SIDE POSITIONING
        float xPos = lastWasLeft ? -sideOffset : sideOffset;
        currentWeapon.transform.localPosition = new Vector3(xPos, verticalOffset, 0);

        if (lastWasLeft)
            currentWeapon.transform.localRotation = Quaternion.Euler(0, 0, -90f);
        else
            currentWeapon.transform.localRotation = Quaternion.Euler(0, 180, -90f);

        // 3. SORTING (The only thing that changes when moving Up)
        if (weaponSR != null)
        {
            // Tucks behind the player when moving up, stays in front for everything else
            weaponSR.sortingOrder = (direction == "Up") ? playerBaseOrder - 1 : playerBaseOrder + 1;
        }
    }
}