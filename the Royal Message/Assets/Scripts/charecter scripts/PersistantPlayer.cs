using UnityEngine;

public class PersistentPlayer : MonoBehaviour
{
    public static PersistentPlayer Instance;

    void Awake()
    {
        // This stops the Player from being destroyed when the scene changes
        DontDestroyOnLoad(gameObject);

        // This prevents "Double Players" if you go back to a previous scene
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}