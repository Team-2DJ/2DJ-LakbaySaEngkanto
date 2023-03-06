using UnityEngine;
using UnityEngine.EventSystems;

public class BookSlot : MonoBehaviour, IDropHandler, IPointerExitHandler
{
    [SerializeField] string bookTitle;
    private bool isOccupied;
    public bool IsRight { get; private set; }

    private void OnEnable()
    {
        SingletonManager.Get<GameEvents>().OnPlayerPlacedItem += CheckAnswer;
    }

    private void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnPlayerPlacedItem -= CheckAnswer;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            isOccupied = true;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition
            = GetComponent<RectTransform>().anchoredPosition;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            isOccupied = false;
            IsRight = false;
        }
    }

    private void CheckAnswer(string bookTitle)
    {
        if (!isOccupied) return;
        if (bookTitle != this.bookTitle) return;

        IsRight = true;
    }
}
