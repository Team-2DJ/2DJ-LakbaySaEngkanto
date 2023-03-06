using UnityEngine;
using UnityEngine.EventSystems;

public class BookSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] string bookTitle;
    public bool IsRight { get; private set; }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<BookPiece>().GetBookTitle() == bookTitle)
            {
                IsRight = true;

                Debug.Log(IsRight);
            }

            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition
            = GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
