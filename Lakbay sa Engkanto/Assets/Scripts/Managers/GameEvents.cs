using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Manages all C# Events
/// </summary>
public class GameEvents : MonoBehaviour
{
    public event Action<float> OnPlayerDamaged;                                             // Called When Player Takes Damage
    public event Action OnUpdateUI;                                                         // Called to Update Game UI

    public event Action<string> OnPlayerCollectItem;

    public event Action<string, int, int> OnScoreChanged;

    public event Action OnOpenDoors;
    public event Action OnCloseDoors;

    void Awake()
    {
        SingletonManager.Register(this);
    }

    public void PlayerDamaged(float value)
    {
        OnPlayerDamaged?.Invoke(value);
        OnUpdateUI?.Invoke();
    }

    public void PlayerCollectItem(string id)
    {
        OnPlayerCollectItem?.Invoke(id);
    }

    public void ScoreChanged(string id, int currentPoints, int winningPoints)
    {
        OnScoreChanged?.Invoke(id, currentPoints, winningPoints);
    }

    public void OpenDoors()
    {
        OnOpenDoors?.Invoke();
    }

    public void CloseDoors()
    {
        OnCloseDoors?.Invoke();
    }
}
