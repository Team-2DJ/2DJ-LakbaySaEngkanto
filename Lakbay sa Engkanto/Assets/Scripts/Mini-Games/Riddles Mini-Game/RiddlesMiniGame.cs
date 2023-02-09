using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RiddlesMiniGame : MiniGame
{
    [System.Serializable]
    struct RidddleItems
    {
        public string Question;
        public Sprite Answer;
    }

    [Header("Game Events")]
    [SerializeField] private string correctID;
    [SerializeField] private string wrongID;


    [Header("Mini Game Setup")]
    [SerializeField] private List<RidddleItems> riddles;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private AnswerItem[] multipleChoiceAnswers;

    private Animator Animator;
    private List<RidddleItems> riddlesInPlay = new List<RidddleItems>();
    private bool hasStarted;
    private bool hasEnded;

    [Header("Win Conditions")]
    private int correctAnswerScore;
    [SerializeField] private int totalScore;

    // Start is called before the first frame update
    void Start()
    {
        SingletonManager.Get<GameEvents>().OnPlayerCollectItem += CheckAnswer;
        PlayerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();
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
        hasStarted = true;

        // Chooses the correct Riddle based on the elements found within the array
        RidddleItems correctRiddle = riddlesInPlay[Random.Range(0, riddlesInPlay.Count)];

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
        foreach (RidddleItems ridddleItems in riddles)
            riddlesInPlay.Add(ridddleItems);

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
                //SingletonManager.Get<GameEvents>().PlayerCollectItem(id);

                Debug.Log("YOU WIN!!!");
                Animator.SetTrigger("isComplete");

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

    private void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnPlayerCollectItem -= CheckAnswer;
    }
}
