using UnityEngine;

public class HotbarManager : MonoBehaviour
{
    [Header("Weapon Slots")]
    public GameObject[] weapons; // Slot 0: Sword, Slot 1: Crossbow

    private int currentSlot = 0;

    void Start()
    {
        // Ensure only the first weapon is active at the start
        SelectSlot(0);
    }

    void Update()
    {
        // Keyboard Shortcuts
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(1);

        // Scroll Wheel Support
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
            // Only the selected index stays active
            if (weapons[i] != null)
            {
                weapons[i].SetActive(i == index);
            }
        }
    }
}