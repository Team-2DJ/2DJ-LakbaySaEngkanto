using UnityEngine;
using System.Linq;

public class BooksMiniGame : MonoBehaviour
{
    [Header("Object Setup")]
    [SerializeField] string id;                                     // Object's ID
    [SerializeField] private BookSlot[] bookSlots;                  // BookSlots Array


    [Header("Gameplay Settings")]
    [SerializeField] string doorToOpen;                              // Door To Open string

    public bool IsComplete { get; private set; }                     // IsComplete boolean

    /// <summary>
    /// Checks bookslot order whether they are correct or not. 
    /// </summary>
    public void CheckOrder()
    {
        // If the game is complete then return; 
        if (IsComplete) return;

        // if all bookSlot in the bookSlots array have the right books, 
        // then call the first statement, else if false 
        if (bookSlots.All(bookSlot => bookSlot.IsRight))
        {
            // Open the Door corresponding to this GameObjects ID.
            SingletonManager.Get<GameEvents>().OpenDoor(doorToOpen);

            // Invoke a false boolean that listeners will receive.
            SingletonManager.Get<GameEvents>().SetCondition(id, false);

            IsComplete = true;

            // Turns off this GameObject after completion
            gameObject.SetActive(false);

            // Turns back on PlayerMovement 
            SingletonManager.Get<GameEvents>().SetMovement(true);
        }
        else
        {
            // Invoke a true boolean that listeners will receive. 
            SingletonManager.Get<GameEvents>().SetCondition(id, true);
        }
    }
}
