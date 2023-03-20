using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private ItemData itemData;

    private Image image;

    public void InitializeItem(ItemData newItem)
    {
        image ??= GetComponent<Image>();

        itemData = newItem;
        image.sprite = itemData.GetIcon();
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {

    }

    public void OnDrag(PointerEventData pointerEventData)
    {

    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {

    }

}
