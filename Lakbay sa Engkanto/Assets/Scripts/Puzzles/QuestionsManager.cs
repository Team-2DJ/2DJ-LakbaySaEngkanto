using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionsManager : MonoBehaviour
{
    [SerializeField] AnswerItem[] answers;

    [Header("Sprites")]
    [SerializeField] Sprite rightAnswer;
    [SerializeField] List<Sprite> wrongAnswer = new();

    void Start()
    {
        RandomizeQuestions();
    }

    void RandomizeQuestions()
    {
        var correctAnswer = answers[Random.Range(0, answers.Length)];
        correctAnswer.isTrue = true;
        correctAnswer.GetComponent<SpriteRenderer>().sprite = rightAnswer;

        foreach (var answer in answers)
        {
            if (answer.isTrue) continue;

            answer.isTrue = false;
            int index = Random.Range(0, wrongAnswer.Count);
            answer.GetComponent<SpriteRenderer>().sprite = wrongAnswer[index];
            wrongAnswer.RemoveAt(index);
        }
    }
}
