using UnityEngine;

public class TraderLogic : MonoBehaviour
{
    [Header("Settings")]
    public int killPenalty = -20;
    [Range(0, 100)]
    public float vanishChanceMultiplier = 1.5f;
    public MoralityManager MoralityManager;
    private static bool isQuitting = false;

    void Start()
    {
        // Check IMMEDIATELY when spawned in case the player is already a criminal
        CheckIfIVanish();
    }

    private void OnEnable()
    {
        MoralityManager.OnMoralityChanged += CheckIfIVanish;
    }

    private void OnDisable()
    {
        MoralityManager.OnMoralityChanged -= CheckIfIVanish;
    }

    // This detects if the game is closing so you don't lose morality 
    // just for exiting the level.
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    void CheckIfIVanish()
    {
        if (MoralityManager.MoralityScore < 0)
        {
            float chance = Mathf.Abs(MoralityManager.MoralityScore) * vanishChanceMultiplier;
            if (Random.Range(0, 100) < chance)
            {
                // We use SetActive(false) for vanishing so OnDestroy doesn't trigger!
                gameObject.SetActive(false);
            }
        }
    }

    // This runs automatically when the object is deleted (Killed)
    private void OnDestroy()
    {
        // Only apply penalty if the game isn't closing and the object wasn't just disabled
        if (!isQuitting && gameObject.scene.isLoaded)
        {
            // If the trader is destroyed, it counts as a kill
            MoralityManager.AddMorality(killPenalty);
            Debug.Log("Trader Destroyed: Penalty Applied.");
        }
    }
}