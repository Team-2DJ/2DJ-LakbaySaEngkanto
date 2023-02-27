using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Image image;                                           // Image Component Reference
    [SerializeField] private TextMeshProUGUI characterNameText;                     // Character Name Text Reference
    [SerializeField] private TextMeshProUGUI sentenceText;                          // Sentence Text Reference
    [SerializeField] private float typeSpeed;                                       // Speed for Text-Typing Behavior

    private DialogueData[] dialogueData;                                            // Dialogue Data to be Shown

    private int currentDialogueIndex;                                               // Current Dialogue Index
    private int currentSentenceIndex;                                               // Current Sentence Index

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    /// <summary>
    /// Dialogue Start-Up
    /// </summary>
    /// <param name="dialogue"></param>
    public void InitializeDialogues(DialogueData[] dialogue)
    {
        // Set this Dialogue Data to Dialogue 
        dialogueData = dialogue;

        // Initialize Both Dialogue and Sentence Indexes
        currentDialogueIndex = 0;
        currentSentenceIndex = 0;

        StartDialogue();
    }

    /// <summary>
    /// Initiate the Dialogue
    /// </summary>
    void StartDialogue()
    {
        // Set Character Image and Name
        image.sprite = dialogueData[currentDialogueIndex].CharacterImage;
        characterNameText.text = dialogueData[currentDialogueIndex].Name;

        StopAllCoroutines();
        StartCoroutine(Type());
    }


    // Put Sentence Here
    IEnumerator Type()
    {
        // Set Sentence Text to Blank
        sentenceText.text = "";
        
        // Implement Typing Behavior for the Sentence
        foreach (char s in dialogueData[currentDialogueIndex].Sentences[currentSentenceIndex])
        {
            sentenceText.text += s;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    /// <summary>
    /// Go to the Next Dialogue
    /// </summary>
    void NextDialogue()
    {
        // End Dialogue if there Are No More Dialogues
        if (currentDialogueIndex >= dialogueData.Length - 1)
        {
            // Enable Player Movement
            SingletonManager.Get<GameEvents>().DialogueEnd();

            // Re-Open Game Panel
            SingletonManager.Get<PanelManager>().ActivatePanel("Game Panel", 0f);
            return;
        }

        // Increment Current Dialogue Index
        currentDialogueIndex++;
        
        StartDialogue();
    }

    /// <summary>
    /// Proceed to the Next Sentence
    /// </summary>
    public void NextSentence()
    {
        if (currentSentenceIndex >= dialogueData[currentDialogueIndex].Sentences.Length - 1)
        {
            // Reset Current Sentence Index
            currentSentenceIndex = 0;

            // Proceed with the Next Dialogue
            NextDialogue();

            return;
        }

        // Increment Current Sentence Index
        currentSentenceIndex++;

        StopAllCoroutines();
        StartCoroutine(Type());
    }
}
