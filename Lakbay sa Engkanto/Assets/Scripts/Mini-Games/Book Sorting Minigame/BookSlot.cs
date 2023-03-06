using UnityEngine;
using UnityEngine.EventSystems;

public class BookSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] string bookTitle;
    bool isRight;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<BookPiece>().GetBookTitle() == bookTitle)
            {
                isRight = true;

                Debug.Log(isRight);
            }

            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition
            = GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
