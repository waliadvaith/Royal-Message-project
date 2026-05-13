using UnityEngine;
using TMPro;

public class TalentTreeManager : MonoBehaviour
{
    [Header("Global Settings")]
    public MoralityManager moralityManager;
    public int talentPoints = 3; // Give them some to start!

    [Header("UI References")]
    public GameObject menuPanel;
    public TextMeshProUGUI pointsText;

    void Start()
    {
        menuPanel.SetActive(false);
        UpdateGlobalUI();
    }

    void Update()
    {
        // Toggle menu with 'T'
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        bool isActive = !menuPanel.activeSelf;
        menuPanel.SetActive(isActive);

        // Pause game & Cursor control
        Time.timeScale = isActive ? 0f : 1f;
        Cursor.visible = isActive;
        Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;

        if (isActive) UpdateGlobalUI();
    }

    public void UpdateGlobalUI()
    {
        if (pointsText != null) pointsText.text = "Talent Points: " + talentPoints;

    }
}