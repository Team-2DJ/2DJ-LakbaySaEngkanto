using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages Everything Relating to the Player
public class PlayerManager : MonoBehaviour
{
    public bool IsTesting;                               // For Debugging Purposes

    public Player Player { get; set; }                   // Player Game Object Reference

    public PlayerData PlayerData;                        // Player Data Class Reference

    public InventoryManager PlayerInventory;             // Player Inventory Data Reference

    public Vector2 PlayerSpawnPoint { get; set; }        // Player Spawn Point Coordinates

    public GameObject ChichayPrefab;



    #region Singleton
    void Awake()
    {
        // Register this Class to the Singleton Manager
        SingletonManager.Register(this);
    }
    #endregion

    public void ResetProperties()
    {
        PlayerData.ClearData();
        PlayerInventory.ClearPlayerInventory();

        SingletonManager.Get<DebugEvents>().ResetData();

        // Reset Player Spawn Point
        PlayerSpawnPoint = Vector2.zero;
    }
}
