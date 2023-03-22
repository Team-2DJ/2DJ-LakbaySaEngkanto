using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PodiumMiniGame : MonoBehaviour
{
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private GameObject inventorySlotHolder;

    private List<InventorySlot> inventorySlots = new();

    private void Awake()
    {
        InventorySlot[] tempInventorySlot = inventorySlotHolder?.GetComponentsInChildren<InventorySlot>() ?? new InventorySlot[0];

        foreach (InventorySlot slot in tempInventorySlot)
        {
            inventorySlots.Add(slot);
        }
    }

    private void OnEnable()
    {
        var inventory = SingletonManager.Get<PlayerManager>().PlayerInventory.GetItemDictionary();

        for (int i = 0; i < inventory.Count; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null)
            {
                SpawnInventorySlot(inventory.Keys.ElementAt(i), slot);
                return;
            }
        }
    }

    private void SpawnInventorySlot(ItemData itemData, InventorySlot slot)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(itemData);
    }
}
