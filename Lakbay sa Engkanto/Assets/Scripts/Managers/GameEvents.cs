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

    public event Action OnDialogueStart;
    public event Action OnDialogueEnd;

    public event Action<string> OnPlayerCollectItem, OnPlayerPlacedItem;

    public event Action<String> OnOpenDoor, OnCloseDoor;

    public event Action<bool> OnSetCondition;

    public event Action<string, int, int> OnScoreChanged;

    void Awake()
    {
        /*if (SingletonManager.Contains<GameEvents>())
        {
            Destroy(this.gameObject);
        }
        else
        {
            
            DontDestroyOnLoad(this.gameObject);
        }*/

        SingletonManager.Register(this);
    }

    public void PlayerDamaged(float value)
    {
        OnPlayerDamaged?.Invoke(value);
        OnUpdateUI?.Invoke();
    }

    public void DialogueStart()
    {
        OnDialogueStart?.Invoke();
    }

    public void DialogueEnd()
    {
        OnDialogueEnd?.Invoke();
    }

    public void PlayerCollectItem(string id)
    {
        OnPlayerCollectItem?.Invoke(id);
    }

    public void PlayerPlacedItem(string id)
    {
        OnPlayerPlacedItem?.Invoke(id);
    }

    public void SetCondition(bool condition)
    {
        OnSetCondition?.Invoke(condition);
    }

    public void ScoreChanged(string id, int currentPoints, int winningPoints)
    {
        OnScoreChanged?.Invoke(id, currentPoints, winningPoints);
    }

    public void OpenDoor(string id)
    {
        OnOpenDoor?.Invoke(id);
    }

    public void CloseDoor(string id)
    {
        OnCloseDoor?.Invoke(id);
    }
}
