using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelTeleporter : MonoBehaviour
{
    public string sceneToLoad;
    public Vector3 spawnPositionInNewScene; // Set this in the Inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TeleportSequence(other.gameObject));
        }
    }

    IEnumerator TeleportSequence(GameObject player)
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!loadScene.isDone) { yield return null; }

        player.transform.position = spawnPositionInNewScene;
    }
}