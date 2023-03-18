using UnityEngine;
using System;

[Serializable]
public class InventoryItem
{
    [SerializeField] private ItemData itemData;

    public InventoryItem(ItemData item)
    {
        itemData = item;
    }
}
