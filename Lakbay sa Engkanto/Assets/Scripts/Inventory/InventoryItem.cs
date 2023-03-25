using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// An Item that impelements the ItemData ScriptableObject
/// </summary>
[RequireComponent(typeof(Image))]
public class InventoryItem : MonoBehaviour
{
    public ItemData ItemData { get; private set; }

    private Image image;

    /// <summary>
    /// Initializes The Item
    /// </summary>
    /// <param name ="newItem"> the item that will be initialized </param>
    public void InitializeItem(ItemData newItem)
    {
        image ??= GetComponent<Image>();

        ItemData = newItem;
        image.sprite = ItemData.GetClosedIcon();
    }
}
