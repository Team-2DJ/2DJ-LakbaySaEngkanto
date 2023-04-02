using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Initializes All Player Properties
[RequireComponent(typeof(HealthComponent), typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    public Rigidbody2D Rb { get; private set; }                                             // Rigidbody2D Component Reference
    public HealthComponent HealthComponent { get; private set; }                            // HealthComponent Class Reference
    public PlayerMovement PlayerMovement { get; private set; }                              // PlyerMovement Class Reference

    public Animator Animator;                                                               // Animator Component Reference

    [SerializeField] private Transform chichayFollowPoint;                                  // Point Where Chichay Needs to Go to

    private void OnEnable()
    {
        SingletonManager.Get<PlayerManager>().Player = this;
    }

    private void OnDisable()
    {
        SingletonManager.Get<PlayerManager>().Player = null;
    }

    // Start is called before the first frame update
    void Awake()
    {
        // Cache-In All Variables
        Rb = GetComponent<Rigidbody2D>();
        HealthComponent = GetComponent<HealthComponent>();
        PlayerMovement = GetComponent<PlayerMovement>();
        Animator = GetComponentInChildren<Animator>();

        // Set Position
        if (SingletonManager.Get<PlayerManager>().PlayerSpawnPoint != new Vector2(0f, 0f))
            transform.position = SingletonManager.Get<PlayerManager>().PlayerSpawnPoint;

        // Check If Player Has Encountered Chichay.
        // If so, Spawn Chichay Prefab on Start-Up
        if (SingletonManager.Get<PlayerManager>().PlayerData.HasMetChichay)
            SpawnChichay();
    }

    /// <summary>
    /// Spawns a Prefab of Chichay, Marian's (The Player) Companion
    /// </summary>
    public void SpawnChichay()
    {
        Chichay chichay = Instantiate(SingletonManager.Get<PlayerManager>().ChichayPrefab, transform.position, Quaternion.identity, this.transform).GetComponent<Chichay>();
        chichay.transform.SetParent(null);
        chichay.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        chichay.Initialize(chichayFollowPoint);
    }
}