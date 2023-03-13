using UnityEngine;
using TMPro;

public class PressurePlateMiniGame : MonoBehaviour
{
    [Header("Object Setup")]

    [SerializeField] private TextMeshProUGUI questionTextBox;
    [SerializeField] private string id;
    [SerializeField] private PressurePlate[] pressurePlates;

    [Header("Gameplay Settings")]
    [SerializeField] string questionText;

    private void Start()
    {
        questionTextBox.text = questionText;
    }


    // Might implement a randomzing pressure plate system idk? 

}
