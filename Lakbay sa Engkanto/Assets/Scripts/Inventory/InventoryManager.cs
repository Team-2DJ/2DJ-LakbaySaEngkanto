using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages all things related to inventory
/// </summary>
public class InventoryManager : MonoBehaviour
{
    // A list that contains all of the ItemData values 
    public List<ItemData> ItemDataList { get; private set; } = new();

    /// <summary>
    /// Accepts one parameter, adds the item to the list and
    /// calls an event that adds the item to those that will display it
    /// </summary>
    /// <param name="itemData">The Data being added</param>
    public void AddItem(ItemData itemData)
    {
        if (ItemDataList.Contains(itemData)) return;

        SingletonManager.Get<PlayerEvents>().AddItemToInventory(itemData);

        ItemDataList.Add(itemData);
    }

    /// <summary>
    /// Accepts one parameter, adds the item to the list and
    /// calls an event that adds the item to those that will display it
    /// </summary>
    /// <param name="itemData">The Data being removed</param>
    public void RemoveItem(ItemData itemData)
    {
        if (!ItemDataList.Contains(itemData)) return;

        SingletonManager.Get<PlayerEvents>().RemoveItemFromInventory(itemData);

        ItemDataList.Remove(itemData);
    }
}
