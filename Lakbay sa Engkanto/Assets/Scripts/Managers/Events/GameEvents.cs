using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Manages all Game Events
/// </summary>
public class GameEvents : MonoBehaviour
{
    public event Action<string> OnOpenDoor, OnCloseDoor;                                    // Called by Door Events

    public event Action<string, bool> OnSetCondition;                                       // An event that sets a boolean to those that will use it

    void Awake()
    {
        SingletonManager.Register(this);
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
}
