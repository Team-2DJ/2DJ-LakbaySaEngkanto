using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using TMPro;

public class BookSortingMiniGame : MonoBehaviour
{
    [Header("Object Setup")]
    [SerializeField] private string id;                                     // Object's ID
    [SerializeField] private TextMeshProUGUI categoryTextGUI;               // Category Text GUI
    [SerializeField] private GameObject bookSlotHolder;                     // GameObject that contains all BookSlots
    [SerializeField] private GameObject bookPieceHolder;                    // GameObject that contains all BookPieces


    [Header("Gameplay Settings")]
    [SerializeField] private string doorToOpen;                             // Door To Open string
    [SerializeField] private string bookCategory;                           // Sort Category
    [SerializeField] private List<string> correctBookTitles = new();        // Titles of the different books 
    [SerializeField] private List<string> wrongBookTitles = new();          // Titles of the different books 



    private List<BookSlot> bookSlots = new();                               // BookSlots List 
    private List<BookPiece> bookPieces = new();                             // BookSlots List 

    public bool IsComplete { get; private set; }                            // IsComplete boolean

    void Start()
    {
        BookSlot[] tempBookSlots = bookSlotHolder?.GetComponentsInChildren<BookSlot>() ?? new BookSlot[0];
        BookPiece[] tempBookPieces = bookPieceHolder?.GetComponentsInChildren<BookPiece>() ?? new BookPiece[0];
        categoryTextGUI.text = bookCategory;

        if (!correctBookTitles.Any()) Debug.LogError("NO BOOK TITLES PRESENT");

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

    /// <summary>
    /// Randomizes the game's BookOrder
    /// </summary>
    private void RandomizeBookOrder()
    {
        // For each Book Slot present in the bookSlot list
        // set the bookTitle based on BookSlot[i]

        // e.g. the first element present in the bookSlots list
        // will have the first bookTitle present in the bookTitles list
        for (int i = 0; i < bookSlots.Count; i++)
        {
            bookSlots[i].Initialize(id, correctBookTitles[i]);
        }

        // For each element present in the bookPieces list, 
        // randomize the order of the titles and set it afterwards 

        for (int i = 0; i < correctBookTitles.Count; i++)
        {
            for (int j = 0; j < bookPieces.Count; j++)
            {
                // Index to be used by the BookPiece based on a random value
                int titleIndex = Random.Range(0, correctBookTitles.Count);

                // Index to be used by the BookPiece based on a random value
                int bookIndex = Random.Range(0, bookPieces.Count);

                // Initializes the bookPiece with the random value based on the index
                bookPieces[bookIndex].Initialize(id, correctBookTitles[titleIndex]);

                // Remove the bookTitle from the list   
                bookPieces.RemoveAt(bookIndex);
                correctBookTitles.RemoveAt(titleIndex);
            }
        }

        for (int i = 0; i < wrongBookTitles.Count; i++)
        {
            for (int j = 0; j <= bookPieces.Count; j++)
            {
                // Index to be used by the BookPiece based on a random value
                int titleIndex = Random.Range(0, wrongBookTitles.Count);

                // Index to be used by the BookPiece based on a random value
                int bookIndex = Random.Range(0, bookPieces.Count);

                // Initializes the bookPiece with the random value based on the index
                bookPieces[bookIndex].Initialize(id, wrongBookTitles[titleIndex]);

                // Remove the bookTitle from the list 
                bookPieces.RemoveAt(bookIndex);
                wrongBookTitles.RemoveAt(titleIndex);
            }
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

            // Turns back on Game Panel
            SingletonManager.Get<PanelManager>().ActivatePanel("Game Panel");
        }
        else
        {
            // Invoke a false boolean that listeners will receive. 
            SingletonManager.Get<GameEvents>().SetCondition(id, false);
        }
    }

    public void OnCloseButtonClicked()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Game Panel");
        SingletonManager.Get<PlayerEvents>().SetPlayerMovement(true);
    }

    public string GetID()
    {
        return id;
    }
}
