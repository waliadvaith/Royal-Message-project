using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI; // Needed for the Portrait Image


[System.Serializable]
public class DialogueLine
{
    public string characterName;
    public Sprite characterPortrait;
    [TextArea(3, 10)]
    public string sentence;

    // This creates the "On Line Start" box in the Inspector
    public UnityEvent onLineStart;
}

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [Header("UI References")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image portraitImage; // New: Drag your UI Image here

    private Queue<DialogueLine> lines = new Queue<DialogueLine>();
    public bool isOpen = false;

    void Awake() { instance = this; dialoguePanel.SetActive(false); }

    public void StartDialogue(List<DialogueLine> dialogueList)
    {
        isOpen = true;
        dialoguePanel.SetActive(true);
        lines.Clear();

        foreach (DialogueLine line in dialogueList)
        {
            lines.Enqueue(line);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        // --- NEW: Trigger the specific command for this line ---
        if (currentLine.onLineStart != null)
        {
            currentLine.onLineStart.Invoke();
        }
        // -------------------------------------------------------

        nameText.text = currentLine.characterName;

        if (currentLine.characterPortrait != null)
        {
            portraitImage.sprite = currentLine.characterPortrait;
            portraitImage.gameObject.SetActive(true);
        }
        else
        {
            portraitImage.gameObject.SetActive(false);
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine.sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    void EndDialogue()
    {
        isOpen = false;

        // 1. Clear the text so it's empty for the next time
        dialogueText.text = "";
        nameText.text = "";

        // 2. Clear the portrait so the last person doesn't stay there
        if (portraitImage != null) portraitImage.sprite = null;

        // 3. Hide the panel
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (isOpen && (Input.GetKeyDown(KeyCode.X)))
        {
            DisplayNextSentence();
        }
    }
}