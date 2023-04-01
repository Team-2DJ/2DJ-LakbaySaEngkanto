using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using TMPro;

public class BookSortingMiniGame : MonoBehaviour
{
    [Header("Object Setup")]
    [SerializeField] private TextMeshProUGUI categoryTextGUI;               // Category Text GUI
    [SerializeField] private GameObject bookPiecePrefab, bookSlotPrefab;    // GameObject that contains all BookPieces
    [SerializeField] private Transform bookSlotHolder, bookPieceHolder;     // GameObject that contains all BookSlots


    [Header("Gameplay Settings")]
    private string id;                                     // Object's ID
    private string doorToOpen;                             // Door To Open string
    private string bookCategory;                           // Sort Category
    private List<string> correctBookTitles = new();        // Titles of the different books 
    private List<string> wrongBookTitles = new();          // Titles of the different books 
    private List<BookSlot> bookSlots = new();                               // BookSlots List 
    private List<BookPiece> bookPieces = new();                             // BookSlots List 

    public bool IsComplete { get; private set; }                            // IsComplete boolean

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    public void Initialize(string _id, string _doorToOpen, string _bookCategory, List<string> _correctTitles, List<string> _wrongTitles)
    {
        List<string> tempCorrect = new(_correctTitles);
        List<string> tempWrong = new(_wrongTitles);

        ClearData();

        id = _id;
        doorToOpen = _doorToOpen;
        bookCategory = _bookCategory;
        correctBookTitles = tempCorrect;
        wrongBookTitles = tempWrong;

        categoryTextGUI.text = bookCategory;

        foreach (var number in correctBookTitles)
        {
            GameObject bookSlot = Instantiate(bookSlotPrefab, bookSlotHolder);
            bookSlots.Add(bookSlot.GetComponent<BookSlot>());
        }

        int numberOfBooks = correctBookTitles.Count + wrongBookTitles.Count;

        for (int i = 0; i < numberOfBooks; i++)
        {
            GameObject bookPiece = Instantiate(bookPiecePrefab, bookPieceHolder);
            bookPieces.Add(bookPiece.GetComponent<BookPiece>());
        }

        RandomizeBookOrder();
    }

    private void PopulateBookSlots()
    {
        BookSlot[] tempBookSlots = bookSlotHolder?.GetComponentsInChildren<BookSlot>() ?? new BookSlot[0];

        foreach (BookSlot bookSlot in tempBookSlots)
        {
            bookSlots.Add(bookSlot);
        }
    }

    private void PopulateBookPieces()
    {
        BookPiece[] tempBookPieces = bookPieceHolder?.GetComponentsInChildren<BookPiece>() ?? new BookPiece[0];

        foreach (BookPiece bookPiece in tempBookPieces)
        {
            bookPieces.Add(bookPiece);
        }
    }

    public void ClearData()
    {
        id = null;
        doorToOpen = null;
        bookCategory = null;
        IsComplete = false;

        foreach (var bookSlot in bookSlots)
        {
            Destroy(bookSlot.gameObject);
        }

        foreach (var bookPiece in bookPieces)
        {
            Destroy(bookPiece.gameObject);
        }

        correctBookTitles.Clear();
        wrongBookTitles.Clear();
        bookSlots.Clear();
        bookPieces.Clear();
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
        SetupBookTitles(correctBookTitles);
        SetupBookTitles(wrongBookTitles);
    }

    private void SetupBookTitles(List<string> bookTitles)
    {
        List<string> tempTitles = new(bookTitles);

        for (int i = 0; i < bookPieces.Count; i++)
        {
            int titleIndex = Random.Range(0, tempTitles.Count);
            int bookIndex = Random.Range(0, bookPieces.Count);

            while (bookPieces[bookIndex].hasBeenInitialized)
            {
                bookIndex = Random.Range(0, bookPieces.Count);
            }

            bookPieces[bookIndex].Initialize(id, tempTitles[titleIndex]);
            tempTitles.RemoveAt(titleIndex);

            if (!tempTitles.Any()) break;
        }

        /*
                for (int i = 0; i < tempTitles.Count; i++)
                {
                    for (int j = 0; j < bookPieces.Count; j++)
                    {
                        int titleIndex = Random.Range(0, tempTitles.Count);
                        int bookIndex = Random.Range(0, bookPieces.Count);

                        while (bookPieces[bookIndex].hasBeenInitialized)
                        {
                            bookIndex = Random.Range(0, bookPieces.Count);
                        }

                        bookPieces[bookIndex].Initialize(id, tempTitles[titleIndex]);
                        tempTitles.RemoveAt(titleIndex);

                        if (!tempTitles.Any()) break;
                    }

                    if (!tempTitles.Any()) break;
                }
                */
    }

    /// <summary>
    /// Checks bookslot order whether they are correct or not. 
    /// </summary>
    public void CheckOrder()
    {
        // If the game is complete then return; 
        if (IsComplete)
        {
            Debug.Log("GAME IS COMPLETE");
            return;
        }

        Debug.Log(bookSlots.All(bookSlot => bookSlot.IsRight));

        // if all bookSlot in the bookSlots array have the right books, 
        // then call the first statement, else if false 
        if (bookSlots.All(bookSlot => bookSlot.IsRight))
        {
            // Play Correct SFX
            SingletonManager.Get<AudioManager>().PlayOneShot("Correct");

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

            ClearData();
        }
        else
        {
            // Play Incorrect SFX
            SingletonManager.Get<AudioManager>().PlayOneShot("Incorrect");

            // Invoke a false boolean that listeners will receive. 
            SingletonManager.Get<GameEvents>().SetCondition(id, false);
        }
    }

    public void OnCloseButtonClicked()
    {
        // Play Close SFX
        SingletonManager.Get<AudioManager>().PlayOneShot("Close");

        SingletonManager.Get<PanelManager>().ActivatePanel("Game Panel");
        SingletonManager.Get<PlayerEvents>().SetPlayerMovement(true);
        ClearData();
    }

    public string GetID()
    {
        return id;
    }
}
