using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Manages all C# Events
/// </summary>
public class GameEvents : MonoBehaviour
{
    public event Action<float> OnPlayerDamaged;

    public event Action<float> OnSlowDownPlayer;
    public event Action<float> OnIncreasePlayerSpeed;
    public event Action OnPickupPage;

    public event Action OnPlayerCollectItem;

    void Awake()
    {
        SingletonManager.Register(this);
    }

    public void PlayerDamaged(float value)
    {
        OnPlayerDamaged?.Invoke(value);
    }

    public void SlowDownPlayer(float value)
    {
        OnSlowDownPlayer?.Invoke(value);
    }

    public void IncreasePlayerSpeed(float value)
    {
        OnIncreasePlayerSpeed?.Invoke(value);
    }

    public void PlayerCollectItem()
    {
        OnPlayerCollectItem?.Invoke();
    }

    public void PickupPage()
    {
        OnPickupPage?.Invoke();
    }
}
