using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShuffleTextMiniGame : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image picture;                                         // Picture Reference
    [SerializeField] private Transform letterItemHolder;                            // Holds the Shuffle Letters to be Spawned
    [SerializeField] private TextMeshProUGUI answerText;                            // Answer Text Reference       

    [Header("Prefabs")]
    [SerializeField] private GameObject shuffleLetterPrefab;                        // Prefab for the Shuffle Letter

    private ShuffleTextData shuffleWord;                                            // Contains All ShuffleText Data

    private string correctAnswer;                                                   // The Correct Answer

    private string doorToOpen;                                                      // Door To Open string
    private List<ShuffleLetter> shuffleLetterList = new();                          // Contains All Shuffle Letters
    private List<char> letters = new();                                             // Contains All Characters from the Correct Answer
    private ShuffleTextTrigger shuffleTextTrigger;                                  // Stores the Assigned ShuffleText Trigger Instance
    private bool isEvaluating;                                                      // Indicates if the Mini-Game is Undergoing Evaluation

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    public void Initialize(ShuffleTextData data, ShuffleTextTrigger reference, string doorId)
    {
        // Clear Data Upon Start-Up
        ClearData();

        shuffleWord = data;
        shuffleTextTrigger = reference;
        answerText.text = "";
        doorToOpen = doorId;
        SetQuestion();
        SpawnShuffleLetters();
    }

    /// <summary>
    /// Sets-Up the Question
    /// </summary>
    void SetQuestion()
    {
        // Set Picture and Correct Answer
        picture.sprite = shuffleWord.AnswerImage;
        correctAnswer = shuffleWord.AnswerString;

        // Add Every Letter from the Answer String to the Character List
        foreach (char c in shuffleWord.AnswerString)
            letters.Add(c);

        RandomizeShuffleLetterOrder();
    }

    /// <summary>
    /// Generates a Jumbled Version of the Correct Answer
    /// </summary>
    void RandomizeShuffleLetterOrder()
    {
        for (int i = 0; i < letters.Count; i++)
        {
            char temp = letters[i];

            int rand = Random.Range(i, letters.Count);

            letters[i] = letters[rand];

            letters[rand] = temp;
        }
    }

    /// <summary>
    /// Spawning of the Shuffle Letters
    /// </summary>
    void SpawnShuffleLetters()
    {
        foreach (char c in letters)
        {
            // Spawn the Shuffle Letter Game Object
            // and Get the ShuffleLetter Class Reference
            ShuffleLetter shuffleLetterItem = Instantiate(shuffleLetterPrefab, letterItemHolder.position, Quaternion.identity).GetComponent<ShuffleLetter>();

            // Place it in the Letter Item Holder
            shuffleLetterItem.transform.SetParent(letterItemHolder);

            // Maintain Default Scale
            float scaleValue = 1.5f;
            shuffleLetterItem.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);

            // Add to the List and Set its Name
            shuffleLetterList.Add(shuffleLetterItem);
            shuffleLetterItem.name = c.ToString();

            // Initialize Shuffle Letter Properties
            shuffleLetterItem.Initialize(c, this);
        }
    }

    /// <summary>
    /// Cleans Up All Data so that this Panel can be Reused
    /// </summary>
    void ClearData()
    {
        // Indicate Mini-Game No Longer Evaluating
        isEvaluating = false;

        // Clear Text
        answerText.text = "";

        // Clear ShuffleTextTrigger Reference
        shuffleTextTrigger = null;

        // Clear Shuffle Letter Holder
        foreach (ShuffleLetter letter in shuffleLetterList)
            Destroy(letter.gameObject);

        // Clear All List Data
        shuffleLetterList.Clear();
        letters.Clear();
    }

    /// <summary>
    /// Types the Letter Pressed by the Player to the Answer Text
    /// </summary>
    /// <param name="s"></param>
    public void TypeLetter(char s)
    {
        // Play Button Pressed SFX
        SingletonManager.Get<AudioManager>().PlayOneShot("Button Pressed");

        // Type the Givern Letter to the Answer Text
        answerText.text += s.ToString();

        // Don't Evaluate if Player Didn't Use All Letters
        if (answerText.text.ToCharArray().Length != correctAnswer.ToCharArray().Length)
            return;

        StartCoroutine(Evaluate());
    }

    /// <summary>
    /// Checks if Player is Corrent or Not
    /// </summary>
    /// <returns></returns>
    IEnumerator Evaluate()
    {
        // Inidicate Status to "Evaluating"
        isEvaluating = true;
        
        yield return new WaitForSeconds(0.5f);

        if (answerText.text == correctAnswer)
        {
            // Correct Answer
            Debug.Log("CORRECT");

            answerText.text = "MAHUSAY!!!";

            // Mark the Designated ShuffleTextTrigger as Complete
            shuffleTextTrigger.Completed();

            // Play Correct SFX
            SingletonManager.Get<AudioManager>().PlayOneShot("Correct");

            yield return new WaitForSeconds(1f);

            // Open Door
            SingletonManager.Get<GameEvents>().OpenDoor(doorToOpen);

            // Clear Data
            ClearData();

            // Enable Player Movement and Game Panel
            SingletonManager.Get<PanelManager>().ActivatePanel("Game Panel");
            SingletonManager.Get<PlayerEvents>().SetPlayerMovement(true);
        }
        else
        {
            // Wrong Answer
            Debug.Log("WRONG");

            // Play Incorrect SFX
            SingletonManager.Get<AudioManager>().PlayOneShot("Incorrect");

            answerText.text = "MALI!!!";

            yield return new WaitForSeconds(1f);

            // Evaluating Finished
            isEvaluating = false;

            // Reset All Buttons
            OnResetButtonClicked();
        }
    }

    #region Button Functions
    /// <summary>
    /// Randomizes ShuffleLetter Positions
    /// </summary>
    public void OnShuffleButtonClicked()
    {
        // Don't Execute if Mini-Game is Evaluating
        if (isEvaluating)
            return;

        // Play Shuffle SFX
        SingletonManager.Get<AudioManager>().PlayOneShot("Shuffle");

        // Put All Shuffle Letters in Random Positions
        foreach (ShuffleLetter item in shuffleLetterList)
            item.transform.SetSiblingIndex(Random.Range(0, shuffleLetterList.Count));
    }

    public void OnCloseButtonClicked()
    {
        // Don't Execute if Mini-Game is Evaluating
        if (isEvaluating)
            return;

        // Play Close SFX
        SingletonManager.Get<AudioManager>().PlayOneShot("Close");

        ClearData();
        SingletonManager.Get<PanelManager>().ActivatePanel("Game Panel");
        SingletonManager.Get<PlayerEvents>().SetPlayerMovement(true);
    }

    /// <summary>
    /// Resets the Mini-Game
    /// </summary>
    public void OnResetButtonClicked()
    {
        // Don't Execute if Mini-Game is Evaluating
        if (isEvaluating)
            return;

        // Play Reset SFX
        SingletonManager.Get<AudioManager>().PlayOneShot("Reset");

        // Reset Answer Text
        answerText.text = "";

        // Set All Shuffle Letters Back to Interactable
        foreach (ShuffleLetter item in shuffleLetterList)
            item.SetButtonInteractable(true);
    }
    #endregion
}
