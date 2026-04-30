using UnityEngine;
using UnityEngine.SceneManagement;


public class beginteleport : MonoBehaviour
{ 
    // Make sure the function is PUBLIC so the button can see it
    public void LoadTargetScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

