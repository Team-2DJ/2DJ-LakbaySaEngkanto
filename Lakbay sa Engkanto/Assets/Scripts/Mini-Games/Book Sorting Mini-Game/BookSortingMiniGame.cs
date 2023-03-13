using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class BookSortingMiniGame : MonoBehaviour
{
    [Header("Object Setup")]
    [SerializeField] private string id;                              // Object's ID
    [SerializeField] private GameObject bookSlotHolder;


    [Header("Gameplay Settings")]
    [SerializeField] private string doorToOpen;                      // Door To Open string

    private List<BookSlot> bookSlots = new();                        // BookSlots List 

    public bool IsComplete { get; private set; }                     // IsComplete boolean

    void Start()
    {
        BookSlot[] temp = bookSlotHolder?.GetComponentsInChildren<BookSlot>() ?? new BookSlot[0];

        foreach (BookSlot bookSlot in temp)
        {
            bookSlots.Add(bookSlot);
        }
    }

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

            // Invoke a true boolean that listeners will receive.
            SingletonManager.Get<GameEvents>().SetCondition(id, true);

            IsComplete = true;

            // Turns off this GameObject after completion
            gameObject.SetActive(false);

            // Turns back on PlayerMovement 
            SingletonManager.Get<PlayerEvents>().SetPlayerMovement(true);
        }
        else
        {
            // Invoke a false boolean that listeners will receive. 
            SingletonManager.Get<GameEvents>().SetCondition(id, false);
        }
    }

    public string GetID()
    {
        return id;
    }
}
