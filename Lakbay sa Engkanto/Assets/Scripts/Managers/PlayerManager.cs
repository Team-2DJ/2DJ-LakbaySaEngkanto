using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages Everything Relating to the Player
public class PlayerManager : MonoBehaviour
{
    public PlayerSetup Player;  // Player Game Object Reference

    #region Singleton
    void Awake()
    {
        // Register this Class to the Singleton Manager
        SingletonManager.Register(this);
    }
    #endregion
}
