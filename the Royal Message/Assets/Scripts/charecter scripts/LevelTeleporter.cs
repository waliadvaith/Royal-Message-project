using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelTeleporter : MonoBehaviour
{
    public string sceneToLoad;
    public Vector3 spawnPositionInNewScene;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Keep the player alive across scenes
            DontDestroyOnLoad(other.gameObject);
            StartCoroutine(TeleportSequence(other.gameObject));
        }
    }

    IEnumerator TeleportSequence(GameObject player)
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!loadScene.isDone)
        {
            yield return null;
        }

        // Wait one tiny frame for the new scene to settle
        yield return new WaitForEndOfFrame();

        // FORCE the position
        player.transform.position = spawnPositionInNewScene;

        Debug.Log("Player moved to: " + spawnPositionInNewScene);
    }
}