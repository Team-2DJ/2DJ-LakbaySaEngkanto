using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(InventoryItem))]
public class PodiumBook : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("Object Setup")]
    [SerializeField] private string id;                                     // Object ID
    [SerializeField] private Sprite closedBook, openedBook;                 // Image States 

    [Header("Gameplay Settings")]
    [SerializeField] private string bookTitle;                              // Title of the Book

    #region private variables
    private RectTransform rectTransform;                                    // This objects rectTransform
    private CanvasGroup canvasGroup;                                        // This objects canvasGroup
    private Image image;                                                    // This Object's image 
    private Canvas canvas;                                                  // Canvas Reference
    private InventoryItem inventoryItem;

    private Transform parentTransform;                                      // Reference to Parent Transform; 

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
        inventoryItem = GetComponent<InventoryItem>();

        canvas ??= GetComponentInParent<Canvas>(true);

        // Sets the originalPositions values;
        parentTransform = transform.parent;

        closedBook = inventoryItem.ItemData.GetClosedIcon();
        openedBook = inventoryItem.ItemData.GetOpenedIcon();
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

        transform.SetParent(transform.root);
    }


    /// <summary>
    /// Inherited from IDragHandler
    /// gets called every frame while the object is being dragged
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        // Allows for object movement based on Mouse Position; 
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        // If BookPiece is over PodiumSlot, open the book
        // else, close the book;
        image.sprite = eventData.pointerEnter?.GetComponent<PodiumSlot>() ? openedBook : closedBook;
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

        // Sets the Dropped boolean if target object is a PodiumSlot.
        hasBeenDropped = eventData.pointerEnter?.GetComponent<PodiumSlot>();

        // If the object is not dropped, bring it back to its original position
        // else, call PlayerPlacedItem from GameEvents, using this bookTitle as its parameter. 
        if (!hasBeenDropped)
        {
            transform.SetParent(parentTransform);
            rectTransform.anchoredPosition = Vector2.zero;
        }
        else
        {
            eventData.pointerEnter.GetComponent<PodiumSlot>().CheckAnswer(inventoryItem.ItemData);
            transform.SetParent(eventData.pointerEnter.transform);
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

        transform.SetParent(parentTransform);
        rectTransform.anchoredPosition = Vector2.zero;

        hasBeenDropped = false;

        image.sprite = closedBook;
    }
}