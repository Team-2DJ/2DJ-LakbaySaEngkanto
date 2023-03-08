using UnityEngine;

public enum Type
{
    CORRECT, INCORRECT
}

public class PressurePlate : MonoBehaviour
{

    [Header("Object Setup")]
    [SerializeField] private string id;                                     // Object ID

    [Header("Gameplay Settings")]
    [SerializeField] private Type type;                                     // Pressure Plate Type  

    private bool isPressed;                                                 // isPressed Boolean

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the object has already been pressed, then return
        if (isPressed) return;

        // Add Listener to the Interactable button using OnPressed as a parameter
        SingletonManager.Get<GameEvents>().AddActionListener(OnPressed);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // If the object has already been pressed, then return
        if (isPressed) return;

        // Remove Listener to the Interactable button using OnPressed as a parameter
        SingletonManager.Get<GameEvents>().RemoveActionListener(OnPressed);
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
                SingletonManager.Get<GameEvents>().PlayerDamaged(1f);
                isPressed = true;
                break;
        }
    }
}
