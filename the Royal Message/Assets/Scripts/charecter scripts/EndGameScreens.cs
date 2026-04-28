using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScreens : MonoBehaviour
{
    public GameObject winPanel;  // Drag your "You Win" child object here
    public GameObject losePanel; // Drag your "Game Over" child object here

    void Start()
    {
        // Hide both at the start
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    public void ActivateWin()
    {
        winPanel.SetActive(true);
        FinishGame();
    }

    public void ActivateLose()
    {
        losePanel.SetActive(true);
        FinishGame();
    }

    void FinishGame()
    {
        Time.timeScale = 0f; // Freeze the world
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Add this to your Restart Button's OnClick()
    public void ReloadGame()
    {
        Time.timeScale = 1f;
        // Since Player is DontDestroyOnLoad, we must destroy them 
        // to avoid having two players when the first scene loads
        Destroy(transform.root.gameObject);
        SceneManager.LoadScene(0);
    }
}