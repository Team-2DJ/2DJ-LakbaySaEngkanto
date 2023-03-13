using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BookPiece : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("Object Setup")]
    [SerializeField] private string id;                                     // Object ID
    [SerializeField] private Canvas canvas;                                 // Canvas Reference
    [SerializeField] private Sprite frontSprite, sideSprite;                // Image States 

    [Header("Gameplay Settings")]
    [SerializeField] private string bookTitle;                              // Title of the Book

    #region private variables
    private RectTransform rectTransform;                                    // This objects rectTransform
    private CanvasGroup canvasGroup;                                        // This objects canvasGroup
    private Vector2 originalPosition;                                       // This objects OriginalPosition

    private Image image;                                                    // This Object's image 

    private bool hasBeenDropped;                                            // Dropped Boolean
    #endregion

    private void OnEnable()
    {
        SingletonManager.Get<GameEvents>().OnSetCondition += ResetBookPiece;
    }

    private void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnSetCondition -= ResetBookPiece;
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();

        canvas ??= GetComponentInParent<Canvas>();

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

        if (eventData.pointerEnter?.GetComponent<BookSlot>())
        {
            image.sprite = sideSprite;
            rectTransform.sizeDelta = new Vector2(100, 300);
        }
        else
        {
            image.sprite = frontSprite;
            rectTransform.sizeDelta = new Vector2(215, 300);
        }
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
        hasBeenDropped = eventData.pointerEnter?.GetComponent<BookSlot>();

        // If the object is not dropped, bring it back to its original position
        // else, call PlayerPlacedItem from GameEvents, using this bookTitle as its parameter. 

        if (!hasBeenDropped)
            rectTransform.anchoredPosition = originalPosition;
        else
        {
            SingletonManager.Get<PlayerEvents>().PlayerPlacedItem(bookTitle);
        }
    }

    /// <summary>
    /// Resets the BookPiece back to its initial values
    /// </summary>
    /// <param name="dontReset">conditional</param>
    private void ResetBookPiece(string id, bool dontReset)
    {
        if (id != this.id) return;
        if (dontReset) return;

        rectTransform.anchoredPosition = originalPosition;
        hasBeenDropped = false;

        image.sprite = frontSprite;
        rectTransform.sizeDelta = new Vector2(215, 300);
    }
}