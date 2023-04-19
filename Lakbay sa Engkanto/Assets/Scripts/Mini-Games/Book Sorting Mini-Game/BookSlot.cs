using UnityEngine;
using UnityEngine.EventSystems;

public class BookSlot : MonoBehaviour, IDropHandler, IPointerExitHandler
{
    [Header("Object Setup")]
    [SerializeField] private string id;

    [Header("Gameplay Settings")]
    [SerializeField] private string bookTitle;

    private bool isOccupied;
    public bool IsRight { get; private set; }
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(string _id, string _bookTitle)
    {
        id = _id;
        bookTitle = _bookTitle;
    }

    private void OnEnable()
    {
        SingletonManager.Get<PlayerEvents>().OnPlayerPlacedItem += CheckAnswer;
        SingletonManager.Get<GameEvents>().OnSetCondition += ResetBookSlot;
    }

    private void OnDisable()
    {
        SingletonManager.Get<PlayerEvents>().OnPlayerPlacedItem -= CheckAnswer;
        SingletonManager.Get<GameEvents>().OnSetCondition -= ResetBookSlot;
    }

    /// <summary>
    /// Inherited from IDropHandler, 
    /// checks if an object is dropped above this gameObject.
    /// </summary>
    public void OnDrop(PointerEventData eventData)
    {
        // if an object is dropped, then pointerDrag != null
        if (eventData.pointerDrag != null)
        {
            isOccupied = true;

            RectTransform droppedObject = eventData.pointerDrag.GetComponent<RectTransform>();

            // sets droppedObject position == this objects position; 
            droppedObject.anchoredPosition = rectTransform.anchoredPosition;
        }
    }

    /// <summary>
    /// Inherited from IPointerExitHandler, 
    /// checks if an overlapping object leaves this gameObject.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.childCount <= 0)
        {
            isOccupied = false;
            IsRight = false;
        }
    }



    /// <summary>
    /// Checks if the book currently placed in this gameObject is correct. 
    /// </summary>
    /// <param name="bookTitle">The Title of the Book</param>
    private void CheckAnswer(string bookTitle)
    {
        // if the bookSlot is currently not occupied, then return; 
        if (!isOccupied) return;

        // if the title of the book doesn't correspond with this gameObject, then return; 
        if (bookTitle != this.bookTitle) return;

        IsRight = true;
    }

    /// <summary>
    /// Resets the BookSlot back to its initial values
    /// </summary>
    /// <param name="dontReset">conditional</param>
    private void ResetBookSlot(string id, bool dontReset)
    {
        if (id != this.id) return;
        if (dontReset) return;

        isOccupied = false;
        IsRight = false;
    }
}
