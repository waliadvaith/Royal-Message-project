using UnityEngine;
using UnityEngine.SceneManagement;


public class beginteleport : MonoBehaviour
{ 
    public void LoadTargetScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

