using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameScreens : MonoBehaviour
{
    public GameObject winPanel;  // Drag your "You Win" child object here
    public GameObject losePanel; // Drag your "Game Over" child object here

    public GameObject restartButton;


    void Start()
    {
        // Hide both at the start
        winPanel.SetActive(false);
        losePanel.SetActive(false);

        restartButton.SetActive(false);

    }

    public void ActivateWin()
    {
        winPanel.SetActive(true);
        restartButton.SetActive(true);
        FinishGame();
    }

    public void ActivateLose()
    {
        losePanel.SetActive(true);

        restartButton.SetActive(true);
        FinishGame();
    }

    void FinishGame()
    {
        Time.timeScale = 0f; // Freeze the world
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    
}