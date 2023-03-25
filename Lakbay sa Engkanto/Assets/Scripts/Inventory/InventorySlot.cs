using UnityEngine.EventSystems;
using UnityEngine;

/// <summary>
/// Slot that will show the Items in Inventory
/// </summary>
public class InventorySlot : MonoBehaviour
{
    public RectTransform rectTransform { get; private set; }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

}
