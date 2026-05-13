using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TraderLogic : MonoBehaviour
{
    [Header("Settings")]
    public int killPenalty = -20;
    [Range(0, 100)]
    public float vanishChanceMultiplier = 1.5f;
    public MoralityManager MoralityManager;

    [Header("Departure Dialogue")]
    public List<DialogueLine> leavingDialogue;

    private static bool isQuitting = false;
    private bool isLeaving = false; // Local safety: prevents this specific trader from running logic twice
    private static bool isGlobalDialogueActive = false; // Static safety: prevents multiple traders from talking at once

    void Start()
    {
        // Safety: If the player starts at -100 morality, we wait a frame 
        // to ensure the DialogueManager is initialized.
        Invoke("CheckIfIVanish", 0.1f);
    }

    private void OnEnable()
    {
        if (MoralityManager != null)
            MoralityManager.OnMoralityChanged += CheckIfIVanish;
    }

    private void OnDisable()
    {
        if (MoralityManager != null)
            MoralityManager.OnMoralityChanged -= CheckIfIVanish;
    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    void CheckIfIVanish()
    {
        // 1. Safety Check: If already leaving, or the game is quitting, or another trader is talking... skip.
        if (isLeaving || isQuitting || isGlobalDialogueActive) return;

        if (MoralityManager != null && MoralityManager.MoralityScore < 0)
        {
            float chance = Mathf.Abs(MoralityManager.MoralityScore) * vanishChanceMultiplier;
            if (Random.Range(0, 100) < chance)
            {
                StartCoroutine(LeaveSequence());
            }
        }
    }

    private IEnumerator LeaveSequence()
    {
        isLeaving = true;
        isGlobalDialogueActive = true;

        if (DialogueManager.instance != null && leavingDialogue != null && leavingDialogue.Count > 0)
        {
            DialogueManager.instance.StartDialogue(leavingDialogue);

            // We use a small delay in REAL TIME to make sure the manager has 
            // actually flipped the 'isOpen' switch before we start checking it.
            yield return new WaitForSecondsRealtime(0.1f);

            // This loop checks the status of the dialogue box
            // It keeps running even if Time.timeScale is 0!
            while (DialogueManager.instance.isOpen)
            {
                yield return null;
            }
        }

        // Small extra pause so he doesn't vanish the INSTANT the text disappears
        yield return new WaitForSecondsRealtime(0.2f);

        isGlobalDialogueActive = false;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (!isQuitting && gameObject.scene.isLoaded)
        {
            if (MoralityManager != null)
                MoralityManager.AddMorality(killPenalty);
        }
    }
}