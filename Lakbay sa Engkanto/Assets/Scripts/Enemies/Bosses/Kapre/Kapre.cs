using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class Kapre : MonoBehaviour
{
    enum Direction
    {
        LEFT = -1,
        RIGHT = 1
    }
    
    [Header("References")]
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;                 // Cinemachine Camera Reference
    
    [Header("Camera Shaking Values")]
    [SerializeField] private float amplitude;                                                   // Amplitude Value
    [SerializeField] private float frequency;                                                   // Frequency Value

    [Header("Properties")]
    [SerializeField] private string id;
    [SerializeField] private Direction direction;                                               // Direction Assigner
    [SerializeField] private float xOffset;                                                     // Amount of Offset for the X-Axis
    [SerializeField] private float movementDuration;                                            // Dictates how long the Kapre will Move to the
                                                                                                // Assigned Destination

    private bool isTriggered;                                                                   // Indicates if Player has Collided with This Kapre
    private SpriteRenderer spriteRenderer;                                                      // Sprite Renderer Component Reference
    private Collider2D playerCollider;                                                          // Player Collider2D Reference
    private Animator animator;                                                                  // Aniamtor Component Reference

    private void OnEnable()
    {
        SingletonManager.Get<GameEvents>().OnSeedCollected += TriggerCalm;
    }

    private void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnSeedCollected -= TriggerCalm;
    }

    private void Start()
    {
        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (SingletonManager.Get<PlayerManager>().PlayerData.StringList.Contains(id))
        {
            SetShaking(0f, 0f);
            Destroy(gameObject);
            return;
        }

        SetShaking(amplitude, frequency);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTriggered)
            return;
        
        if (other == playerCollider)
        {
            Flip();

            isTriggered = true;

            // Add this Kapre to the KapreList
            SingletonManager.Get<PlayerManager>().PlayerData.AddString(id);

            // Trigger Walking Animation
            animator.SetTrigger("isWalking");
            
            // Disable Player Movement
            SingletonManager.Get<PlayerEvents>().SetPlayerMovement(false);
            
            // Tween Kapre to Designated Direction
            transform.DOMoveX(transform.position.x + (xOffset * (float)direction), movementDuration).OnComplete(Deactivate);
        }
    }

    /// <summary>
    /// Flip Sprite
    /// </summary>
    void Flip()
    {
        switch(direction)
        {
            case Direction.LEFT:
                spriteRenderer.flipX = false;
                break;

            case Direction.RIGHT:
                spriteRenderer.flipX = true;
                break;
        }
    }

    /// <summary>
    /// Set Shaking Values for Current Camera
    /// </summary>
    /// <param name="amp"></param>
    /// <param name="freq"></param>
    void SetShaking(float amp, float freq)
    {
        // Get Reference to the Cinemachine Multi Channel Perlin
        CinemachineBasicMultiChannelPerlin perlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
        // Set Amplitude and Frequency
        perlin.m_AmplitudeGain = amp;
        perlin.m_FrequencyGain = freq;
    }

    /// <summary>
    /// Deactivate Kapre
    /// </summary>
    void Deactivate()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Game Panel");

        // Enable Player Movement
        SingletonManager.Get<PlayerEvents>().SetPlayerMovement(true);
        
        // Neutralize Shaking Values
        SetShaking(0f, 0f);
        
        // Destroy this gameObject
        Destroy(gameObject);
    }

    /// <summary>
    /// Indicates Calm Actions from the Kapre
    /// </summary>
    void TriggerCalm()
    {
        // Neutralize Shaking Values
        SetShaking(0f, 0f);

        animator.SetTrigger("isIdle");
    }
}
