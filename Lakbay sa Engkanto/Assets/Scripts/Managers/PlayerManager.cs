using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages Everything Relating to the Player
public class PlayerManager : MonoBehaviour
{
    public PlayerSetup Player;                           // Player Game Object Reference

    public PlayerData PlayerData;                        // Player Data Class Reference

    public InventoryManager PlayerInventory;             // Player Inventory Data Reference

    public Vector2 PlayerSpawnPoint { get; set; }        // Player Spawn Point Coordinates

    public GameObject ChichayPrefab;

    public bool HasMetChichay { get; set; }

    #region Singleton
    void Awake()
    {
        // Register this Class to the Singleton Manager
        SingletonManager.Register(this);
    }
    #endregion

    public void ResetProperties()
    {
        // Reset Player Spawn Point
        PlayerSpawnPoint = Vector2.zero;

        HasMetChichay = false;
    }
}
