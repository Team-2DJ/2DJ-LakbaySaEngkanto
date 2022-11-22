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

    [SerializeField] RidddleItems[] Riddles;
    [SerializeField] TextMeshProUGUI QuestionText;
    [SerializeField] AnswerItem[] MultipleChoiceAnswers;

    void Start()
    {
        RandomizeQuestions();
    }

    void RandomizeQuestions()
    {
        int randomRiddle = Random.Range(0, Riddles.Length);

        Debug.Log(randomRiddle);

        QuestionText.text = Riddles[randomRiddle].Question;

        MultipleChoiceAnswers[0].GetComponent<Image>().sprite = Riddles[randomRiddle].Answer;

        foreach (var answer in MultipleChoiceAnswers)
        {

        }

        /*var correctAnswer = answers[Random.Range(0, answers.Length)];
        correctAnswer.isTrue = true;
        correctAnswer.GetComponent<SpriteRenderer>().sprite = rightAnswer;

        foreach (var answer in answers)
        {
            if (answer.isTrue) continue;

            answer.isTrue = false;
            int index = Random.Range(0, wrongAnswer.Count);
            answer.GetComponent<SpriteRenderer>().sprite = wrongAnswer[index];
            wrongAnswer.RemoveAt(index);
        }*/
    }
}
