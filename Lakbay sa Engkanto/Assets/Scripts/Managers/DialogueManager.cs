using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private TextMeshProUGUI sentenceText;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private float typeSpeed;

    private DialogueData[] dialogueData;

    private int currentDialogueIndex;
    private int currentSentenceIndex;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Set Dialogue
    public void InitializeDialogues(DialogueData[] dialogue)
    {
        dialogueData = dialogue;

        currentDialogueIndex = 0;
        currentSentenceIndex = 0;

        StartDialogue();
    }

    // Put Start Dialogue Here
    void StartDialogue()
    {
        image.sprite = dialogueData[currentDialogueIndex].CharacterImage;
        characterNameText.text = dialogueData[currentDialogueIndex].Name;

        StopAllCoroutines();
        StartCoroutine(Type());
    }


    // Put Sentence Here
    IEnumerator Type()
    {
        sentenceText.text = "";
        
        foreach (char s in dialogueData[currentDialogueIndex].Sentences[currentSentenceIndex])
        {
            sentenceText.text += s;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    void NextDialogue()
    {
        if (currentDialogueIndex >= dialogueData.Length - 1)
        {
            // End Dialogue
            SingletonManager.Get<PanelManager>().ActivatePanel("Game Panel", 0f);
            return;
        }

        currentDialogueIndex++;
        StartDialogue();
    }

    public void NextSentence()
    {
        if (currentSentenceIndex >= dialogueData[currentDialogueIndex].Sentences.Length - 1)
        {
            // Next Dialogue

            // Reset Current Sentence Index
            currentSentenceIndex = 0;

            NextDialogue();
            return;
        }

        currentSentenceIndex++;

        StopAllCoroutines();
        StartCoroutine(Type());
    }
}
