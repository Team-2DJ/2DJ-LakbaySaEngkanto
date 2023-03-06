using UnityEngine;
using UnityEngine.EventSystems;

public class BookSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] string bookTitle;
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
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition
            = GetComponent<RectTransform>().anchoredPosition;
        }
    }

    private void CheckAnswer(string bookTitle)
    {
        if (bookTitle != this.bookTitle) return;

        IsRight = true;
    }
}
