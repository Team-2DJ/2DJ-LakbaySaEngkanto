using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventorySlot[] inventorySlots;
    [SerializeField] private GameObject inventoryItemPrefab;

    private Dictionary<ItemData, InventoryItem> itemDictionary = new();

    public Dictionary<ItemData, InventoryItem> GetItemDictionary()
    {
        return itemDictionary;
    }

    public void AddItem(ItemData itemData)
    {
        if (itemDictionary.ContainsKey(itemData)) return;

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null)
            {
                SpawnInventorySlot(itemData, slot);
                return;
            }
        }
    }

    public void RemoveItem(ItemData itemData)
    {
        if (!itemDictionary.ContainsKey(itemData)) return;

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot.ItemData == itemData)
            {
                RemoveInventorySlot(itemData);
                return;
            }
        }
    }

    private void SpawnInventorySlot(ItemData itemData, InventorySlot slot)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(itemData);

        itemDictionary.Add(itemData, inventoryItem);
    }

    private void RemoveInventorySlot(ItemData itemData)
    {
        InventoryItem value;
        if (itemDictionary.TryGetValue(itemData, out value))
        {
            itemDictionary.Remove(itemData);
            Destroy(value.gameObject);
        }
    }
}
