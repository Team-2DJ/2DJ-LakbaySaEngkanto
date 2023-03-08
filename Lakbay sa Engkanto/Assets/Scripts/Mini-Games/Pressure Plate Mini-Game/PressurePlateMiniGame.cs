using UnityEngine;
using TMPro;

public class PressurePlateMiniGame : MonoBehaviour
{
    [Header("Object Setup")]
    [SerializeField] private PressurePlate[] pressurePlates;
    [SerializeField] private TextMeshProUGUI questionTextBox;
    [SerializeField] private string id;

    [Header("Gameplay Settings")]
    [SerializeField] string questionText;

    private void Start()
    {
        questionTextBox.text = questionText;
    }



}
