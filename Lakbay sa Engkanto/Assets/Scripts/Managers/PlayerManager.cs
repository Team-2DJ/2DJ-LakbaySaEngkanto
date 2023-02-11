using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages Everything Relating to the Player
public class PlayerManager : MonoBehaviour
{
    public PlayerSetup Player;                           // Player Game Object Reference

    public PlayerData PlayerData;                        // Player Data Class Reference

    public Vector2 PlayerSpawnPoint { get; set; }

    #region Singleton
    void Awake()
    {
        // Register this Class to the Singleton Manager
        SingletonManager.Register(this);
    }
    #endregion
}
