using UnityEngine;
using System;

/// <summary>
/// Manages all Game Events
/// </summary>
public class GameEvents : MonoBehaviour
{
    public event Action<string> OnOpenDoor, OnCloseDoor;                                    // Called by Door Events

    public event Action<string, bool> OnSetCondition;                                       // An event that sets a boolean to those that will use it

    public event Action<string, int, int> OnScoreChanged;                                   // TO BE REMOVED

    public event Action<ItemData> OnAddItemToInventory, OnRemoveItemFromInventory;

    void Awake()
    {
        SingletonManager.Register(this);
    }

    public void AddItemToInventory(ItemData itemData)
    {
        OnAddItemToInventory?.Invoke(itemData);
    }

    public void RemoveItemFromInventory(ItemData itemData)
    {
        OnRemoveItemFromInventory?.Invoke(itemData);
    }

    public void OpenDoor(string id)
    {
        OnOpenDoor?.Invoke(id);
    }

    public void CloseDoor(string id)
    {
        OnCloseDoor?.Invoke(id);
    }

    public void SetCondition(string id, bool condition)
    {
        OnSetCondition?.Invoke(id, condition);
    }

    //  TO BE REMOVED ONCE GAME IS FINALIZED    
    public void ScoreChanged(string id, int currentPoints, int winningPoints)
    {
        OnScoreChanged?.Invoke(id, currentPoints, winningPoints);
    }
}
