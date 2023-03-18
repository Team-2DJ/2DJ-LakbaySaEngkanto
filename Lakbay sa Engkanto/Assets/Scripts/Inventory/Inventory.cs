using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    private List<InventoryItem> inventoryItems = new();
    private Dictionary<ItemData, InventoryItem> itemDictionary = new();

    private void OnEnable()
    {
        SingletonManager.Get<PlayerEvents>().OnPlayerCollectPodiumBook += AddToInventory;
    }

    private void OnDisable()
    {
        SingletonManager.Get<PlayerEvents>().OnPlayerCollectPodiumBook -= AddToInventory;
    }

    private void AddToInventory(ItemData itemData)
    {
        if (itemDictionary.ContainsKey(itemData))
        {
            return;
        }

        InventoryItem newItem = new InventoryItem(itemData);
        inventoryItems.Add(newItem);
        itemDictionary.Add(itemData, newItem);
    }

    private void RemoveFromInventory(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            inventoryItems.Remove(item);
            itemDictionary.Remove(itemData);
        }
    }

}
