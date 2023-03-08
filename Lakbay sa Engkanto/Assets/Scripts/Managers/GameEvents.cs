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

    public event Action<string> OnPlayerCollectItem, OnPlayerPlacedItem;                    // Called by player when collecting or placing an item

    public event Action<string> OnOpenDoor, OnCloseDoor;                                    // Called by Door Events

    public event Action<string, bool> OnSetCondition;                                       // An event that sets a boolean to those that will use it 

    public event Action<Action> OnAddActionListener, OnRemoveActionListener;                // Used to add listners to those who need it

    public event Action<string, int, int> OnScoreChanged;

    // Letter Bridge Mini-Game Events
    public event Action<float> OnSetLetterProbability;

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
        UpdateUI();
    }

    public void UpdateUI()
    {
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

    public void SetCondition(string id, bool condition)
    {
        OnSetCondition?.Invoke(id, condition);
    }

    public void AddActionListener(Action action)
    {
        OnAddActionListener.Invoke(action);
    }

    public void RemoveActionListener(Action action)
    {
        OnRemoveActionListener.Invoke(action);
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

    public void SetLetterProbability(float chance)
    {
        OnSetLetterProbability?.Invoke(chance);
    }
}
