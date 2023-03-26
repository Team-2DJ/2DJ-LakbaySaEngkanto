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
    private bool isOccupied;
    public bool IsRight { get; private set; }

    private void OnEnable()
    {
        SingletonManager.Get<GameEvents>().OnSetCondition += ResetBookSlot;
    }

    private void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnSetCondition -= ResetBookSlot;
    }

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
            isOccupied = true;

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
            isOccupied = false;
            IsRight = false;
        }
    }

    /// <summary>
    /// Checks if the book currently placed in this gameObject is correct. 
    /// </summary>
    /// <param name="itemData">The Podiums ItemData</param>
    public void CheckAnswer(ItemData itemData)
    {
        // if the bookSlot is currently not occupied, then return; 
        if (!isOccupied) return;
        // if the itemData of the book doesn't correspond with this itemData, then return; 
        if (itemData != this.itemData) return;

        IsRight = true;
    }

    /// <summary>
    /// Resets the BookSlot back to its initial values
    /// </summary>
    /// <param name="dontReset">conditional</param>
    private void ResetBookSlot(string id, bool dontReset)
    {
        if (id != this.id) return;
        if (dontReset) return;

        isOccupied = false;
        IsRight = false;
    }

    public ItemData GetItemData()
    {
        return itemData;
    }
}
