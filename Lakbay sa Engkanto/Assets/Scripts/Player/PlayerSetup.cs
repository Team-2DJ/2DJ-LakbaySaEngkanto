using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Initializes All Player Properties
[RequireComponent(typeof(HealthComponent), (typeof(PlayerInventory)), typeof(PlayerMovement))]
public class PlayerSetup : MonoBehaviour
{
    // Rigidbody2D Component Reference
    public Rigidbody2D Rb { get; private set; }
    public HealthComponent HealthComponent { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        // Cache-In All Variables
        Rb = GetComponent<Rigidbody2D>();
        HealthComponent = GetComponent<HealthComponent>();
        PlayerMovement = GetComponent<PlayerMovement>();
    }
}