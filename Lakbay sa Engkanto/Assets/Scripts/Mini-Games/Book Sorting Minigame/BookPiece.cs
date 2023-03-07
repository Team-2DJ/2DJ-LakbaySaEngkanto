using UnityEngine;
using UnityEngine.EventSystems;

public class BookPiece : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;                                 // Canvas Reference
    [SerializeField] private string bookTitle;                              // Title of the Book

    private RectTransform rectTransform;                                    // This objects rectTransform
    private CanvasGroup canvasGroup;                                        // This objects canvasGroup
    private Vector2 originalPosition;                                       // This objects OriginalPosition

    private bool Dropped;                                                   // Dropped Boolean

    private void OnEnable()
    {
        SingletonManager.Get<GameEvents>().OnSetCondition += ResetBookPiece;
    }

    private void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnSetCondition -= ResetBookPiece;
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        // Sets the originalPositions values;
        originalPosition = (Vector2)rectTransform.anchoredPosition;

    }

    /// <summary>
    /// Inherited from IBeginDragHandler
    /// gets called once this object begins to get dragged. 
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Makes the object appear more translucent once clicked. 
        canvasGroup.alpha = 0.6f;

        // Allows for collision
        canvasGroup.blocksRaycasts = false;
    }


    /// <summary>
    /// Inherited from IDragHandler
    /// gets called every frame while the object is being dragged
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        // Allows for object movement based on Mouse Position; 
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    /// <summary>
    /// Inherited from IEndDragHandler
    /// gets called once the player stops dragging the object
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        // Sets back the objects original Alpha
        canvasGroup.alpha = 1f;

        // Disables collision
        canvasGroup.blocksRaycasts = true;

        // Sets the Dropped boolean if target object is a BookSlot.
        Dropped = eventData.pointerEnter?.GetComponent<BookSlot>();

        // If the object is not dropped, bring it back to its original position
        // else, call PlayerPlacedItem from GameEvents, using this bookTitle as its parameter. 

        if (!Dropped)
            rectTransform.anchoredPosition = originalPosition;
        else
        {
            SingletonManager.Get<GameEvents>().PlayerPlacedItem(bookTitle);
        }
    }

    /// <summary>
    /// Resets the BookPiece back to its initial values
    /// </summary>
    /// <param name="reset">conditional</param>
    private void ResetBookPiece(bool reset)
    {
        if (!reset) return;

        rectTransform.anchoredPosition = originalPosition;
        Dropped = false;
    }

}