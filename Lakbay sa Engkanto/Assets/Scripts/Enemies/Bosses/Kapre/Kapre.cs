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
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    [Header("Camera Shaking Values")]
    [SerializeField] private float amplitude;
    [SerializeField] private float frequency;

    [Header("Properties")]
    [SerializeField] private Direction direction;
    [SerializeField] private float xOffset;
    [SerializeField] private float movementDuration;
    
    private Collider2D playerCollider;
    private Animator animator;

    private void Start()
    {
        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        SetShaking(amplitude, frequency);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            animator.SetTrigger("isWalking");
            SingletonManager.Get<PlayerEvents>().SetPlayerMovement(false);
            

            transform.DOMoveX((transform.position.x + xOffset) * (float)direction, movementDuration).OnComplete(Deactivate);
        }
    }

    void SetShaking(float amp, float freq)
    {
        CinemachineBasicMultiChannelPerlin perlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
        perlin.m_AmplitudeGain = amp;
        perlin.m_FrequencyGain = freq;
    }

    void Deactivate()
    {
        SingletonManager.Get<PlayerEvents>().SetPlayerMovement(true);
        SetShaking(0f, 0f);
        Destroy(gameObject);
    }

}
