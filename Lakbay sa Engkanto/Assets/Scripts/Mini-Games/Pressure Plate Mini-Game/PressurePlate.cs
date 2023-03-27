using UnityEngine;
using TMPro;

public enum Type
{
    NONE,
    CORRECT,
    INCORRECT
}

public class PressurePlate : MonoBehaviour
{
    [Header("Object Setup")]
    [SerializeField] private string id;                                     // Object ID
    [SerializeField] private TextMeshProUGUI textBox;                       // TextBox GUI
    [SerializeField] private Sprite pressedSprite;                          // Object Pressed Sprite

    [Header("Gameplay Settings")]
    [SerializeField] private string doorToOpen;                             // Door to Open

    public Type PressurePlateType { get; private set; }                     // Pressure Plate Type  
    private bool isPressed;                                                 // isPressed Boolean

    private void OnEnable()
    {
        SingletonManager.Get<GameEvents>().OnSetCondition += ObjectPressed;
    }

    private void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnSetCondition -= ObjectPressed;
    }

    void Start()
    {
        textBox.gameObject.SetActive(false);
    }

    public void SetID(string value)
    {
        id = value;
    }

    /// <summary>
    /// Initializes the Pressure Plate to be used by the PressurePlateMiniGame
    /// </summary>
    /// <param name = "_id">id to be used by the pressurePlate</param>
    /// <param name = "_type">the PressurePlate type based on an enum value</param>
    /// <param name = "answerText">the answerText the PressurePlate will use</param>
    public void Initialize(string _id, Type _type, string answerText)
    {
        // Assigns the Type value based on the parameter _type
        PressurePlateType = _type;

        // If type == Type.CORRECT id = parameter _id
        // else id = null; 
        doorToOpen = PressurePlateType == Type.CORRECT ? doorToOpen = _id : doorToOpen = null;

        // Sets the textBox text based on the Answer Text; 
        textBox.text = answerText;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the object has already been pressed, then return
        if (isPressed) return;

        textBox.gameObject.SetActive(true);

        // Add Listener to the Interactable button using OnPressed as a parameter
        SingletonManager.Get<UIEvents>().AddButtonListener(OnPressed);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // If the object has already been pressed, then return
        if (isPressed) return;

        textBox.gameObject.SetActive(false);

        // Remove Listener to the Interactable button using OnPressed as a parameter
        SingletonManager.Get<UIEvents>().RemoveButtonListener(OnPressed);
    }

    /// <summary>
    /// Void Function that checks whether the Pressure Plate
    /// type is correct or not
    /// </summary>
    private void OnPressed()
    {
        switch (PressurePlateType)
        {
            case Type.CORRECT:
                // Opens the door based from the doorToOpen value
                SingletonManager.Get<GameEvents>().OpenDoor(doorToOpen);

                // Sets all the pressurePlates that has the same ID as this pressurePlate's isPressed to true
                SingletonManager.Get<GameEvents>().SetCondition(id, true);

                // Changes the TextBox text if the type is correct
                textBox.text = "TAMA!!!";

                break;
            case Type.INCORRECT:
                SingletonManager.Get<PlayerEvents>().PlayerDamaged(1f);
                isPressed = true;
                break;
        }
    }

    /// <summary>
    /// Function used by an event that sets isPressed either true or false; 
    /// </summary>
    private void ObjectPressed(string id, bool condition)
    {
        if (id != this.id) return;

        isPressed = condition;
        GetComponent<SpriteRenderer>().sprite = pressedSprite;
    }
}
