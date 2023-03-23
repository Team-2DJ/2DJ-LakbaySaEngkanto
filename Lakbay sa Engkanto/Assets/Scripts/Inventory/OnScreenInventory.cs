using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenInventory : MonoBehaviour
{
    [SerializeField] private InventorySlot[] inventorySlots;
    [SerializeField] private GameObject inventoryItemPrefab;

    private Dictionary<ItemData, GameObject> itemDictionary = new();

    private void OnEnable()
    {
        SingletonManager.Get<PlayerEvents>().OnAddItemToInventory += ShowItem;
        SingletonManager.Get<PlayerEvents>().OnRemoveItemFromInventory += RemoveItem;
    }

    private void OnDisable()
    {
        SingletonManager.Get<PlayerEvents>().OnAddItemToInventory -= ShowItem;
        SingletonManager.Get<PlayerEvents>().OnRemoveItemFromInventory -= RemoveItem;
    }

    private void ShowItem(ItemData itemData)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null)
            {
                SpawnInventoryItem(itemData, slot);
                return;
            }
        }
    }

    private void RemoveItem(ItemData itemData)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot.ItemData == itemData)
            {
                RemoveInventoryItem(itemData);
                return;
            }
        }
    }

    private void SpawnInventoryItem(ItemData itemData, InventorySlot slot)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(itemData);

        itemDictionary.TryAdd(itemData, newItem);
    }

    private void RemoveInventoryItem(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out GameObject value))
        {
            Destroy(value.gameObject);
            itemDictionary.Remove(itemData);
        }
    }
}
