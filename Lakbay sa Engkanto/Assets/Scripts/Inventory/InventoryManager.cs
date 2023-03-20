using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventorySlot[] inventorySlots;
    [SerializeField] private GameObject inventoryItemPrefab;
    private List<ItemData> itemsInInventory = new();

    public bool AddItem(ItemData itemData)
    {
        if (itemsInInventory.Contains(itemData)) return false;

        itemsInInventory.Add(itemData);

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null)
            {
                SpawnNewItem(itemData, slot);
                return true;
            }
        }

        return false;
    }

    private void SpawnNewItem(ItemData itemData, InventorySlot slot)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(itemData);
    }
}
