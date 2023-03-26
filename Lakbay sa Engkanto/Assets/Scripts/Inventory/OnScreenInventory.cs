using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

/// <summary>
/// On Screen UI for the player Inventory 
/// </summary>
public class OnScreenInventory : MonoBehaviour
{
    [SerializeField] private InventorySlot[] inventorySlots;                // Array of Inventory Slots that will be used
    [SerializeField] private GameObject inventoryItemPrefab;                // The item that will spawn in the InventorySlots

    private Dictionary<ItemData, GameObject> itemDictionary = new();        // An ItemDictionary that stores the gameObjects that will be instantiated

    private void OnEnable()
    {
        var itemDataList = SingletonManager.Get<PlayerManager>().PlayerInventory.ItemDataList;

        foreach (ItemData itemData in itemDataList)
        {
            ShowItem(itemData);
        }

        SingletonManager.Get<PlayerEvents>().OnAddItemToInventory += ShowItem;
        SingletonManager.Get<PlayerEvents>().OnRemoveItemFromInventory += RemoveItem;
    }

    private void OnDisable()
    {
        // Once object gets disabled, destroy all InventoryItem found within the scene
        foreach (var item in itemDictionary)
        {
            Destroy(item.Value);
        }

        // Clears the ItemDictionary
        itemDictionary.Clear();

        SingletonManager.Get<PlayerEvents>().OnAddItemToInventory -= ShowItem;
        SingletonManager.Get<PlayerEvents>().OnRemoveItemFromInventory -= RemoveItem;
    }

    /// <summary>
    /// Shows Items in the inventory slots
    /// </summary>
    /// <param name = "itemData"> Used as a Key for the Dictionary </param> 
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

    /// <summary>
    /// Removes Items in the inventory slots
    /// </summary>
    /// <param name = "itemData"> Used as a Key for the Dictionary </param> 
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

    /// <summary>
    /// Spawns items in the Inventory Slot based on the inventoryItemPrefab
    /// </summary>
    /// <param name = "itemData"> Used for intialization and as a key in the Dictionary </param> 
    /// <param name = "slot">  Will be used as a parent of an item being instantiated </param> 
    private void SpawnInventoryItem(ItemData itemData, InventorySlot slot)
    {
        // Instantiate a newItem using inventoryItemPrefab, 
        // and set the parent using the slot parameter
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);

        // Gets the InventoryItem component from the newItem
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();

        // Initializes the new Item using the itemdata as its parameter
        inventoryItem.InitializeItem(itemData);

        // Tries to add the object to the Dictionary if it doesn't exist yet
        itemDictionary.TryAdd(itemData, newItem);
    }

    /// <summary>
    /// Spawns items in the Inventory Slot based on the inventoryItemPrefab
    /// </summary>
    /// <param name = "itemData"> Used as a Key by the dictionary </param> 
    private void RemoveInventoryItem(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out GameObject value))
        {
            Destroy(value.gameObject);
            itemDictionary.Remove(itemData);
        }

        StartCoroutine(RearrangeInventory());
    }

    /// <summary> 
    /// Re-arranges the Players inventory after an object has been removed 
    /// </summary>
    private IEnumerator RearrangeInventory()
    {
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            // If there is no itemInSlot, Re-arrange the items in the itemDictionary 
            if (itemInSlot == null)
            {
                foreach (var item in itemDictionary.Values.Reverse())
                {
                    itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                    if (itemInSlot != null) break;

                    GameObject go = item;

                    // set the GameObjects parent to slot.transform when the count is > 1
                    // else then set it to inventorySlot[0]
                    go.transform.SetParent(itemDictionary.Count > 1 ? slot.transform : inventorySlots[0].transform);
                    go.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
                break;
            }
        }
    }
}
