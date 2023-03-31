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
    [SerializeField] private Animator arrowAnimator;

    private DialogueData[] dialogueData;                                            // Dialogue Data to be Shown

    private int currentDialogueIndex;                                               // Current Dialogue Index
    private int currentSentenceIndex;                                               // Current Sentence Index
    private bool isTyping;                                                          // Indicator if a Sentence is Being Typed

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
        // End Dialogue Immediately if Dialogue Data has No Content
        if (dialogueData.Length - 1 < 0)
        {
            EndDialogue();
            return;
        }
        
        // Set Character Image and Name
        characterNameText.text = dialogueData[currentDialogueIndex].Name;

        StopAllCoroutines();
        StartCoroutine(Type());
    }


    // Put Sentence Here
    IEnumerator Type()
    {
        isTyping = true;
        
        // Set Sentence Text to Blank
        sentenceText.text = "";

        image.sprite = dialogueData[currentDialogueIndex].CharacterDialogues[currentSentenceIndex].Image;

        // Disable Next Sentence Indicator
        arrowAnimator.SetBool("isFinished", false);

        // Implement Typing Behavior for the Sentence
        foreach (char s in dialogueData[currentDialogueIndex].CharacterDialogues[currentSentenceIndex].Sentences)
        {
            sentenceText.text += s;
            SingletonManager.Get<AudioManager>().PlayOneShot(dialogueData[currentDialogueIndex].AudioID);
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;

        // Enable Next Sentence Indicator
        arrowAnimator.SetBool("isFinished", true);
    }

    /// <summary>
    /// Skips Typing Sequence of Sentence and Shows the Entire Sentence Completely
    /// </summary>
    public void Skip()
    {
        // Skip Typing
        if (isTyping)
        {
            // Stop Typing Coroutine
            StopAllCoroutines();

            // Set the Sentence Text
            sentenceText.text = dialogueData[currentDialogueIndex].CharacterDialogues[currentSentenceIndex].Sentences;

            // Enable Next Sentence Indicator
            arrowAnimator.SetBool("isFinished", true);

            isTyping = false;
        }
        // Proceed to the Next Sentence
        else
        {
            NextSentence();
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
            EndDialogue();
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
        if (currentSentenceIndex >= dialogueData[currentDialogueIndex].CharacterDialogues.Length - 1)
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

    /// <summary>
    /// End the Dialogue
    /// </summary>
    void EndDialogue()
    {
        // Enable Player Movement
        SingletonManager.Get<PlayerEvents>().SetPlayerMovement(true);

        // Re-Open Game Panel
        SingletonManager.Get<PanelManager>().ActivatePanel("Game Panel");
    }
}
