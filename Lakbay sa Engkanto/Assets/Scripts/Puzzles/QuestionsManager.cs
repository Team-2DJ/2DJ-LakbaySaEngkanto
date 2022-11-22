using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionsManager : MonoBehaviour
{
    [System.Serializable]
    struct RidddleItems
    {
        public string Question;
        public Sprite Answer;
    }

    [SerializeField] List<RidddleItems> Riddles;
    [SerializeField] TextMeshProUGUI QuestionText;
    [SerializeField] AnswerItem[] MultipleChoiceAnswers;

    void Start()
    {
        RandomizeQuestions();
    }


    /// <summary>
    /// Randomizes the questions found witihin the game based on the values 
    /// added from Riddles and MultipleChoiceAnswers
    /// </summary>
    void RandomizeQuestions()
    {
        // Chooses the correct Riddle based on the elements found within the array
        RidddleItems correctRiddle = Riddles[Random.Range(0, Riddles.Count)];

        // Changes the Question text based on the correct riddle picked by the system 
        // and deletes it from the array afterwards
        QuestionText.text = correctRiddle.Question;
        Riddles.Remove(correctRiddle);

        // Chooses the AnswerItem that would represent the Correct Riddle and sets it true. 
        AnswerItem correctAnswer = MultipleChoiceAnswers[Random.Range(0, MultipleChoiceAnswers.Length)];
        correctAnswer.isTrue = true;

        // Get's the image sprite from correctRiddle.answer's data 
        correctAnswer.GetComponent<Image>().sprite = correctRiddle.Answer;

        foreach (AnswerItem answer in MultipleChoiceAnswers)
        {
            // if the answer.isTrue iterate (skip) over it
            if (answer.isTrue) continue;

            answer.isTrue = false;
            int index = Random.Range(0, Riddles.Count);

            // For each wrong answer, get the sprite "Answer" found within the Riddles array
            // Afterwards, remove the riddle from the riddles list
            answer.GetComponent<Image>().sprite = Riddles[index].Answer;
            Riddles.RemoveAt(index);
        }
    }
}
