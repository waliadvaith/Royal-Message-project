using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class Choice
{
    public string choiceText;
    public UnityEvent onChoiceSelected;
    public List<DialogueLine> followUpDialogue; // Leads to a new set of lines
}

[System.Serializable]
public class DialogueLine
{
    public string characterName;
    public Sprite characterPortrait;
    [TextArea(3, 10)]
    public string sentence;
    public UnityEvent onLineStart; // Commands to run when this line appears
    public List<Choice> choices;   // Leave empty if no choices for this line
}

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [Header("UI References")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image portraitImage;

    [Header("Choice Settings")]
    public GameObject choiceContainer;    // Parent with Vertical Layout Group
    public GameObject choiceButtonPrefab; // Button prefab with TMP text

    private Queue<DialogueLine> lines = new Queue<DialogueLine>();
    private List<Choice> currentChoices = new List<Choice>();
    public bool isOpen = false;
    private bool isTyping = false;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        dialoguePanel.SetActive(false);
        if (choiceContainer != null) choiceContainer.SetActive(false);
    }

    public void StartDialogue(List<DialogueLine> dialogueList)
    {
        if (dialogueList == null || dialogueList.Count == 0) return;

        StopAllCoroutines();
        isOpen = true;
        dialoguePanel.SetActive(true);
        ClearChoices();

        // --- NEW: Freeze Time ---
        Time.timeScale = 0f;
        // ------------------------

        lines.Clear();
        foreach (DialogueLine line in dialogueList)
        {
            lines.Enqueue(line);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // Prevent skipping if we are currently typing or showing choices
        if (isTyping || (currentChoices != null && currentChoices.Count > 0)) return;

        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        // 1. Trigger Line Event
        currentLine.onLineStart?.Invoke();

        // 2. Set Name and Portrait
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

        // 3. Store choices for after typing finishes
        currentChoices = currentLine.choices;

        // 4. Start Typing
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine.sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(0.02f);
        }
        isTyping = false;

        // Show choices if they exist for this specific line
        if (currentChoices != null && currentChoices.Count > 0)
        {
            ShowChoices();
        }
    }

    void ShowChoices()
    {
        choiceContainer.SetActive(true);
        foreach (Choice choice in currentChoices)
        {
            GameObject btnObj = Instantiate(choiceButtonPrefab, choiceContainer.transform);
            btnObj.GetComponentInChildren<TextMeshProUGUI>().text = choice.choiceText;

            // This captures the choice correctly for the button click
            Choice capturedChoice = choice;
            btnObj.GetComponent<Button>().onClick.AddListener(() => SelectChoice(capturedChoice));
        }
    }

    public void SelectChoice(Choice choice)
    {
        choice.onChoiceSelected?.Invoke();
        ClearChoices();

        if (choice.followUpDialogue != null && choice.followUpDialogue.Count > 0)
        {
            StartDialogue(choice.followUpDialogue);
        }
        else
        {
            DisplayNextSentence();
        }
    }

    void ClearChoices()
    {
        currentChoices = null;
        if (choiceContainer != null)
        {
            foreach (Transform child in choiceContainer.transform) Destroy(child.gameObject);
            choiceContainer.SetActive(false);
        }
    }

    public void EndDialogue()
    {
        isOpen = false;
        isTyping = false;
        dialogueText.text = "";
        nameText.text = "";
        dialoguePanel.SetActive(false);

        // --- NEW: Unfreeze Time ---
        Time.timeScale = 1f;
        // --------------------------
    }

    void Update()
    {
        // Advance dialogue on Click/Space only if NOT typing and NOT showing choices
        if (isOpen && !isTyping && (currentChoices == null || currentChoices.Count == 0))
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                DisplayNextSentence();
            }
        }
    }
}