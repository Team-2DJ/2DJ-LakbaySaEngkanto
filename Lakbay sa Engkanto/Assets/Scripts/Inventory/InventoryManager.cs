using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryItemPrefab;

    public List<ItemData> ItemDataList { get; private set; } = new();

    public void AddItem(ItemData itemData)
    {
        if (ItemDataList.Contains(itemData)) return;

        SingletonManager.Get<GameEvents>().AddItemToInventory(itemData);

        ItemDataList.Add(itemData);
    }

    public void RemoveItem(ItemData itemData)
    {
        if (!ItemDataList.Contains(itemData)) return;

        SingletonManager.Get<GameEvents>().RemoveItemFromInventory(itemData);

        ItemDataList.Remove(itemData);
    }
}
