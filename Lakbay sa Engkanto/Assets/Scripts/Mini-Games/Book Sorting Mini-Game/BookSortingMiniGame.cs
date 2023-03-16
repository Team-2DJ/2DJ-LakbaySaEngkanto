using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class BookSortingMiniGame : MonoBehaviour
{
    [Header("Object Setup")]
    [SerializeField] private string id;                              // Object's ID
    [SerializeField] private GameObject bookSlotHolder;              // GameObject that contains all BookSlots
    [SerializeField] private GameObject bookPieceHolder;             // GameObject that contains all BookPieces


    [Header("Gameplay Settings")]
    [SerializeField] private string doorToOpen;                      // Door To Open string
    [SerializeField] private List<string> bookTitles = new();        // Titles of the different books 

    private List<BookSlot> bookSlots = new();                        // BookSlots List 
    private List<BookPiece> bookPieces = new();                      // BookSlots List 

    public bool IsComplete { get; private set; }                     // IsComplete boolean

    void Start()
    {
        BookSlot[] tempBookSlots = bookSlotHolder?.GetComponentsInChildren<BookSlot>() ?? new BookSlot[0];
        BookPiece[] tempBookPieces = bookPieceHolder?.GetComponentsInChildren<BookPiece>() ?? new BookPiece[0];

        foreach (BookSlot bookSlot in tempBookSlots)
        {
            bookSlots.Add(bookSlot);
        }

        foreach (BookPiece bookPiece in tempBookPieces)
        {
            bookPieces.Add(bookPiece);
        }

        RandomizeBookOrder();
    }

    private void RandomizeBookOrder()
    {
        for (int i = 0; i < bookSlots.Count; i++)
        {
            bookSlots[i].Initialize(id, bookTitles[i]);
        }

        List<string> titlesToBeUsed = new(bookTitles);
        for (int i = 0; i < bookPieces.Count; i++)
        {
            int index = Random.Range(0, titlesToBeUsed.Count);
            bookPieces[i].Initialize(id, titlesToBeUsed[index]);
            titlesToBeUsed.RemoveAt(index);
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
