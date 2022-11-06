using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Manages all C# Events
/// </summary>
public class GameEvents : MonoBehaviour
{
    public Action<float> PlayerDamaged;

    public Action<float> SlowDownPlayer;
    public Action<float> IncreasePlayerSpeed;

    void Awake()
    {
        SingletonManager.Register(this);
    }
}
