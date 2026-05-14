using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    public GameObject win;
    public GameObject lose;
    public GameObject button;
    public Health HealthScript;
    public MoralityManager Morality;
    public GameObject player;
    public void RestartGame()
    {
        Destroy(player);
        Time.timeScale = 1.0f;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        win.SetActive(false);
        lose.SetActive(false);
        button.SetActive(false);
        HealthScript.currentHealth = HealthScript.maxHealth;
        Morality.MoralityScore = 0;
    }
}
