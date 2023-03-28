using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(RectTransform), typeof(CanvasGroup), typeof(Image))]
public class BookPiece : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("Object Setup")]
    [SerializeField] private string id;                                     // Object ID
    [SerializeField] private Sprite frontSprite, sideSprite;                // Image States 
    [SerializeField] private TextMeshProUGUI frontText, sideText;           // Book's title box

    [Header("Gameplay Settings")]
    [SerializeField] private string bookTitle;                              // Title of the Book

    #region private variables
    private Transform parentTransform;
    private RectTransform rectTransform;                                    // This objects rectTransform
    private CanvasGroup canvasGroup;                                        // This objects canvasGroup
    private Vector2 originalSize;                                           // This objects OriginalPosition
    private Image image;                                                    // This Object's image 
    private Canvas canvas;                                                  // Canvas Reference

    private bool hasBeenDropped;                                            // Dropped Boolean
    public bool hasBeenInitialized { get; private set; }                    // Initialized Bool
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
        canvas ??= GetComponentInParent<Canvas>(true);

        // Enables the FrontText GameObject
        frontText.gameObject.SetActive(true);

        parentTransform = transform.parent;

        originalSize = (Vector2)rectTransform.sizeDelta;
    }

    public void Initialize(string _id, string _bookTitle)
    {
        hasBeenInitialized = true;
        id = _id;
        bookTitle = _bookTitle;

        frontText.text = frontText ? bookTitle : null;
        sideText.text = sideText ? bookTitle : null;
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

        // If BookPiece is over BookSlot, face the book sideways
        // else, face the book to the front; 
        if (eventData.pointerEnter?.GetComponent<BookSlot>())
        {
            sideText.gameObject.SetActive(true);
            frontText.gameObject.SetActive(false);

            image.sprite = sideSprite;
            rectTransform.sizeDelta = new Vector2(100, 300);
        }
        else
        {
            frontText.gameObject.SetActive(true);
            sideText.gameObject.SetActive(false);

            image.sprite = frontSprite;
            rectTransform.sizeDelta = originalSize;
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
        {
            transform.SetParent(parentTransform);
            rectTransform.anchoredPosition = Vector2.zero;
        }
        else
        {
            SingletonManager.Get<PlayerEvents>().PlayerPlacedItem(bookTitle);
            transform.SetParent(eventData.pointerEnter.transform);
            transform.position = transform.parent.position;
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

        hasBeenDropped = false;

        image.sprite = frontSprite;
        frontText.gameObject.SetActive(true);
        sideText.gameObject.SetActive(false);

        transform.SetParent(parentTransform);
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.sizeDelta = originalSize;
    }
}