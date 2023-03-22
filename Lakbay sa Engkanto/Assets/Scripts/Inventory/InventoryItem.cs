using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventoryItem : MonoBehaviour
{
    public ItemData ItemData { get; private set; }

    private Image image;

    public void InitializeItem(ItemData newItem)
    {
        image ??= GetComponent<Image>();

        ItemData = newItem;
        image.sprite = ItemData.GetIcon();
    }
}
