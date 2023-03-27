using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class PressurePlateMiniGame : MonoBehaviour
{
    [Header("Object Setup")]
    [SerializeField] private string id;
    [SerializeField] private TextMeshProUGUI questionTextBox;
    [SerializeField] private PressurePlate[] pressurePlates;

    [Space]
    [Header("Gameplay Settings")]
    [SerializeField] private string doorToOpen;
    [SerializeField] string questionText;
    [SerializeField] string correctAnswer;
    [SerializeField] private List<string> wrongAnswers = new();

    private void Start()
    {
        questionTextBox.text = questionText;

        foreach (var pressurePlate in pressurePlates)
            pressurePlate.SetID(id);

        RandomizePressurePlateAnswers();
    }

    /// <summary>
    /// Randomizes the answer each pressure plate has
    /// </summary>
    private void RandomizePressurePlateAnswers()
    {
        // Gets a random pressurePlate from the pressurePlate array;
        int correctIndex = Random.Range(0, pressurePlates.Length - 1);

        // Gets the pressurePlate from the correctIndex int and
        // then initializes it to be correct
        pressurePlates[correctIndex]?.Initialize(doorToOpen, Type.CORRECT, correctAnswer);

        // This code block is used by the pressure plates which are wrong
        foreach (PressurePlate pressurePlate in pressurePlates)
        {
            // If the pressurePlate is correct iterate (skip) over it
            if (pressurePlate.PressurePlateType == Type.CORRECT) continue;

            int index = Random.Range(0, wrongAnswers.Count);

            // Initializes the wrong answers found within the array 
            pressurePlate.Initialize(doorToOpen, Type.INCORRECT, wrongAnswers[index]);
            wrongAnswers.RemoveAt(index);
        }
    }

}
