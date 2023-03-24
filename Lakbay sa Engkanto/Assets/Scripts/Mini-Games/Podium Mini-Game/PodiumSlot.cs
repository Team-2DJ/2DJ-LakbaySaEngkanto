using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class PodiumSlot : MonoBehaviour, IDropHandler, IPointerExitHandler
{
    [Header("Object Setup")]
    [SerializeField] private string id;

    [Header("Gameplay Settings")]
    [SerializeField] private ItemData itemData;

    private RectTransform rectTransform;
    public bool IsRight { get; private set; }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(string id, ItemData itemData)
    {
        this.id = id;
        this.itemData = itemData;
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

            RectTransform droppedObject = eventData.pointerDrag.GetComponent<RectTransform>();

            // sets droppedObject position == this objects position; 
            droppedObject.sizeDelta = rectTransform.sizeDelta;
            droppedObject.anchoredPosition = rectTransform.anchoredPosition;
        }
    }

    /// <summary>
    /// Inherited from IPointerExitHandler, 
    /// checks if an overlapping object leaves this gameObject.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
        }
    }

    /// <summary>
    /// Checks if the book currently placed in this gameObject is correct. 
    /// </summary>
    /// <param name="bookTitle">The Title of the Book</param>
    public void CheckAnswer(ItemData itemData)
    {
        // if the title of the book doesn't correspond with this gameObject, then return; 
        if (itemData != this.itemData) return;

        IsRight = true;
    }

    public ItemData GetItemData()
    {
        return itemData;
    }
}
