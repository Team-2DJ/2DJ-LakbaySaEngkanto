using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Initializes All Player Properties
[RequireComponent(typeof(HealthComponent), typeof(PlayerMovement))]
public class PlayerSetup : MonoBehaviour
{
    public Rigidbody2D Rb { get; private set; }                                             // Rigidbody2D Component Reference
    public HealthComponent HealthComponent { get; private set; }                            // HealthComponent Class Reference
    public PlayerMovement PlayerMovement { get; private set; }                              // PlyerMovement Class Reference

    public Animator Animator;                                                               // Animator Component Reference

    // TO BE REMOVED
    private void OnEnable()
    {
        SingletonManager.Get<PlayerManager>().Player = this;
    }

    // TO BE REMOVED
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
    }
}