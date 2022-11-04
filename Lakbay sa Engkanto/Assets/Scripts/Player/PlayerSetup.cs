using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Initializes All Player Properties
public class PlayerSetup : MonoBehaviour
{
    public Rigidbody2D Rb { get; private set; }                 // Rigidbody2D Component Reference

    // Start is called before the first frame update
    void Start()
    {
        // Cache-In All Variables
        Rb = GetComponent<Rigidbody2D>();
    }
}