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

    private bool lastWasLeft = false;

    void Start()
    {
        SelectSlot(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectSlot(2); // Added for potions/extras

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) SelectSlot((currentSlot + 1) % weapons.Length);
        else if (scroll < 0f) SelectSlot((currentSlot - 1 + weapons.Length) % weapons.Length);
    }

    public void SelectSlot(int index)
    {
        if (index < 0 || index >= weapons.Length) return;

        // --- ANIMATION GLITCH FIX ---
        // If we are switching to a new weapon, reset its animator state
        if (index != currentSlot && weapons[index] != null)
        {
            Animator anim = weapons[index].GetComponentInChildren<Animator>();
            if (anim != null)
            {
                anim.Rebind();
                anim.Update(0f);
            }
        }

        currentSlot = index;

        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] != null) weapons[i].SetActive(i == index);
        }
    }

    // --- NEW PICKUP LOGIC ---
    public bool AddItemToHotbar(GlobalPickup.ItemType type, int amount)
    {
        // AMMO: Direct to Crossbow
        if (type == GlobalPickup.ItemType.Ammo)
        {
            // This finds the Crossbow even if you're currently holding the Sword
            CrossbowScript cb = GetComponentInChildren<CrossbowScript>(true);
            if (cb != null)
            {
                cb.AddAmmo(amount);
                return true;
            }
        }
        // POTION: To the Potion Holder
        else if (type == GlobalPickup.ItemType.HealthPotion)
        {
            PotionUse pot = GetComponentInChildren<PotionUse>(true);
            if (pot != null)
            {
                pot.potionCount += amount;
                return true;
            }
        }
        return false;
    }

    // --- YOUR ORIGINAL VISUAL LOGIC (FIXES THE ERRORS) ---
    public void UpdateWeaponVisuals(string direction, bool isLeft)
    {
        GameObject currentWeapon = weapons[currentSlot];
        if (currentWeapon == null) return;

        SpriteRenderer weaponSR = currentWeapon.GetComponent<SpriteRenderer>();

        if (direction == "Side")
        {
            lastWasLeft = isLeft;
        }

        float xPos = lastWasLeft ? -sideOffset : sideOffset;
        currentWeapon.transform.localPosition = new Vector3(xPos, verticalOffset, 0);

        if (lastWasLeft)
            currentWeapon.transform.localRotation = Quaternion.Euler(0, 0, -90f);
        else
            currentWeapon.transform.localRotation = Quaternion.Euler(0, 180, -90f);

        if (weaponSR != null)
        {
            weaponSR.sortingOrder = (direction == "Up") ? playerBaseOrder - 1 : playerBaseOrder + 1;
        }
    }
}