using UnityEngine;
using System;

/// <summary>
/// Manages all Player Events
/// </summary>
public class PlayerEvents : MonoBehaviour
{
    public event Action<float> OnPlayerDamaged;                                             // Called When Player Takes Damage

    public event Action<string> OnPlayerCollectItem, OnPlayerPlacedItem;                    // Called by player when collecting or placing an item

    public event Action<ItemData> OnAddItemToInventory, OnRemoveItemFromInventory;

    public event Action<bool> OnSetPlayerMovement;                                          // Player Movement

    void Awake()
    {
        SingletonManager.Register(this);
    }

    public void PlayerDamaged(float value)
    {
        OnPlayerDamaged?.Invoke(value);
    }

    public void PlayerCollectItem(string id)
    {
        OnPlayerCollectItem?.Invoke(id);
    }

    public void AddItemToInventory(ItemData itemData)
    {
        OnAddItemToInventory?.Invoke(itemData);
    }
    public void RemoveItemFromInventory(ItemData itemData)
    {
        OnRemoveItemFromInventory?.Invoke(itemData);
    }

    public void PlayerPlacedItem(string id)
    {
        OnPlayerPlacedItem?.Invoke(id);
    }

    public void SetPlayerMovement(bool condition)
    {
        OnSetPlayerMovement?.Invoke(condition);
    }
}
