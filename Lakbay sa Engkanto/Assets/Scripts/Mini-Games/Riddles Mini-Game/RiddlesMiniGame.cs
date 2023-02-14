using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RiddlesMiniGame : MiniGame
{
    [System.Serializable]
    struct RiddleItems
    {
        public string Question;
        public Sprite Answer;
    }

    [Header("Game Events")]
    [SerializeField] private string correctID;
    [SerializeField] private string wrongID;


    [Header("Mini Game Setup")]
    [SerializeField] private List<RiddleItems> riddles;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private AnswerItem[] multipleChoiceAnswers;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int currentRiddleIndex;
    private Animator Animator;
    private List<RiddleItems> riddlesInPlay = new List<RiddleItems>();

    private bool hasStarted;
    private bool hasEnded;

    [SerializeField] GameObject journalPage;
    [SerializeField] Transform journalPageSpawn;

    [Header("Win Conditions")]
    private int correctAnswerScore;
    [SerializeField] private int totalScore;

    private void OnEnable()
    {
        SingletonManager.Get<GameEvents>().OnPlayerCollectItem += CheckAnswer;
    }

    private void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnPlayerCollectItem -= CheckAnswer;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        Animator = GetComponent<Animator>();
        correctAnswerScore = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == PlayerCollider)
        {
            // if the game has ended, trigger the isComplete animation and then leave the code block
            if (hasEnded)
            {
                Animator.SetTrigger("isComplete");
                return;
            }

            // if the game has started, leave the code block; 
            if (hasStarted) return;

            StartCoroutine(base.StartMiniGame(RandomizeQuestions));
            Debug.Log("Questions changed");
        }
    }

    private void RandomizeQuestions()
    {
        CreateRiddles();

        // Indicates that the Mini game has started;
        Animator.SetBool("hasStarted", true);
        hasStarted = true;

        // Set Indication for the Score
        scoreText.text = correctAnswerScore.ToString() + " / " + totalScore.ToString();

        // Initialize Random Number
        int randomIndex = Random.Range(0, riddlesInPlay.Count);

        // Keep on Randomizing until randomIndex Value is Different from the Previous Value
        while (randomIndex == currentRiddleIndex)
        {
            randomIndex = Random.Range(0, riddlesInPlay.Count);
        }

        // Chooses the correct Riddle based on the elements found within the array
        RiddleItems correctRiddle = riddlesInPlay[randomIndex];

        // Set Current Riddle Index to the Chosen Random Number Index
        currentRiddleIndex = randomIndex;

        // Changes the Question text based on the correct riddle picked by the system 
        // and deletes it from the array afterwards
        questionText.text = correctRiddle.Question;
        riddlesInPlay.Remove(correctRiddle);

        // Chooses the AnswerItem that would represent the Correct Answer (Riddle) and sets it true. 
        AnswerItem correctAnswer = multipleChoiceAnswers[Random.Range(0, multipleChoiceAnswers.Length)];
        correctAnswer.isTrue = true;

        // Get's the image sprite from correctRiddle.answer's data 
        correctAnswer.GetComponent<Image>().sprite = correctRiddle.Answer;

        // Makes sure that no wrong item would repeat in the AnswerItem array by deleting the 
        // selected item's riddleInPlay
        foreach (AnswerItem answer in multipleChoiceAnswers)
        {
            // if the answer.isTrue iterate (skip) over it
            if (answer.isTrue) continue;

            answer.isTrue = false;
            int index = Random.Range(0, riddlesInPlay.Count);

            // For each wrong answer, get the sprite "Answer" found within the Riddles array
            // Afterwards, remove the riddle from the riddles list
            answer.GetComponent<Image>().sprite = riddlesInPlay[index].Answer;
            riddlesInPlay.RemoveAt(index);
        }
    }

    /// <Summary>
    /// Resets the riddlesInPlay by populating the array and setting all
    /// the answers back to false
    /// </Summary>
    private void CreateRiddles()
    {
        foreach (RiddleItems RiddleItems in riddles)
            riddlesInPlay.Add(RiddleItems);

        foreach (AnswerItem answer in multipleChoiceAnswers)
            answer.isTrue = false;
    }

    public void CheckAnswer(string id)
    {
        // if the id value is equals to the correct ID value, then call the code block below it
        // else if the id value equals to the wrong ID value, then call the code block below it 
        if (id == correctID)
        {
            correctAnswerScore++;
            Animator.SetTrigger("isCorrect");

            // if game has finished
            if (correctAnswerScore >= totalScore)
            {
                // Give Journal Page as Award
                StartCoroutine(EndMiniGame());

                Debug.Log("YOU WIN!!!");
                Animator.SetBool("isComplete", true);

                hasEnded = true;
                hasStarted = false;

                return;
            }
        }
        else if (id == wrongID)
        {
            Animator.SetTrigger("isWrong");
        }

        hasStarted = false;
        RandomizeQuestions();
    }

    IEnumerator EndMiniGame()
    {
        yield return new WaitForSeconds(3f);

        // Spawn Journal Page
        Debug.Log("Here's your journal page");

        GameObject go = Instantiate(journalPage, journalPageSpawn.position, Quaternion.identity);
        go.transform.SetParent(journalPageSpawn);

        // Open the Doors
    }
}
