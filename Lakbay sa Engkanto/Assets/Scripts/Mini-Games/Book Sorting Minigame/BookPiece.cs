using UnityEngine;
using UnityEngine.EventSystems;

public class BookPiece : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] Canvas canvas;
    [SerializeField] string bookTitle;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;

    private bool Dropped;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        originalPosition = (Vector2)rectTransform.anchoredPosition;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        Dropped = eventData.pointerEnter?.GetComponent<BookSlot>();


        if (!Dropped)
            rectTransform.anchoredPosition = originalPosition;
        else
        {
            SingletonManager.Get<GameEvents>().PlayerPlacedItem(bookTitle);
        }
    }
}