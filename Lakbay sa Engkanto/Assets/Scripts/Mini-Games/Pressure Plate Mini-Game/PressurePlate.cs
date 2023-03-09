using UnityEngine;
using TMPro;

public enum Type
{
    CORRECT, INCORRECT
}

public class PressurePlate : MonoBehaviour
{

    [Header("Object Setup")]
    [SerializeField] private string id;                                     // Object ID
    [SerializeField] private TextMeshProUGUI textBox;                       // TextBox GUI

    [Header("Gameplay Settings")]
    [SerializeField] private Type type;                                     // Pressure Plate Type  
    [SerializeField] private string text;                                   // Pressure Plate Text 

    private bool isPressed;                                                 // isPressed Boolean

    void Start()
    {
        textBox.text = text;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the object has already been pressed, then return
        if (isPressed) return;

        // Add Listener to the Interactable button using OnPressed as a parameter
        SingletonManager.Get<UIEvents>().AddButtonListener(OnPressed);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // If the object has already been pressed, then return
        if (isPressed) return;

        // Remove Listener to the Interactable button using OnPressed as a parameter
        SingletonManager.Get<UIEvents>().RemoveButtonListener(OnPressed);
    }

    /// <summary>
    /// Void Function that checks whether the Pressure Plate
    /// type is correct or not
    /// </summary>
    private void OnPressed()
    {
        switch (type)
        {
            case Type.CORRECT:
                SingletonManager.Get<GameEvents>().OpenDoor(id);
                isPressed = true;
                break;
            case Type.INCORRECT:
                SingletonManager.Get<PlayerEvents>().PlayerDamaged(1f);
                isPressed = true;
                break;
        }
    }
}
