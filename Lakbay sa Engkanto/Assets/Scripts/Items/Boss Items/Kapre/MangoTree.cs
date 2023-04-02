using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MangoTree : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject lightRain;
    [SerializeField] private GameObject sparkles;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;                 // Cinemachine Camera Reference

    [Header("Camera Shaking Values")]
    [SerializeField] private float amplitude;                                                   // Amplitude Value
    [SerializeField] private float frequency;                                                   // Frequency Value

    public Animator Animator { get; private set; }
    private Collider2D playerCollider;

    private void OnEnable()
    {
        SingletonManager.Get<GameEvents>().OnSeedCollected += StartGrowing;
    }

    private void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnSeedCollected -= StartGrowing;
    }

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();
        sparkles.SetActive(false);
        lightRain.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            StartCoroutine(GoingHome());
        }
    }

    /// <summary>
    /// Initiate Tree Growth Animation
    /// </summary>
    void StartGrowing()
    {
        StartCoroutine(Grow());
    }

    /// <summary>
    /// Indicate Growing Tree
    /// </summary>
    /// <returns></returns>
    IEnumerator Grow()
    {
        // Trigger Growing Animation
        Animator.SetTrigger("isGrowing");

        // Deactivate All Panels
        SingletonManager.Get<PanelManager>().ActivatePanel("");

        // Disable Player Movement
        SingletonManager.Get<PlayerEvents>().SetPlayerMovement(false);

        yield return new WaitForSeconds(3f);

        // Show Sparkles
        sparkles.SetActive(true);

        yield return new WaitForSeconds(3f);

        // Hide Sparkles
        sparkles.SetActive(false);

        // Enable Player Movement
        SingletonManager.Get<PlayerEvents>().SetPlayerMovement(true);

        // Activate Game Panel
        SingletonManager.Get<PanelManager>().ActivatePanel("Game Panel");
    }

    /// <summary>
    /// Indicate End Game
    /// </summary>
    /// <returns></returns>
    IEnumerator GoingHome()
    {
        // Disable Player Movement
        SingletonManager.Get<PlayerEvents>().SetPlayerMovement(false);

        // Shake Camera
        SetShaking(amplitude, frequency);

        // Show Light Rain Effect
        lightRain.SetActive(true);

        // Deactivate All Panels
        SingletonManager.Get<PanelManager>().ActivatePanel("");

        yield return new WaitForSeconds(3f);

        // Fade to White
        SingletonManager.Get<PanelManager>().ActivatePanel("White Panel");

        yield return new WaitForSeconds(1.75f);

        // End the Game
        SingletonManager.Get<GameManager>().EndGame();
    }

    void SetShaking(float amp, float freq)
    {
        // Get Reference to the Cinemachine Multi Channel Perlin
        CinemachineBasicMultiChannelPerlin perlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        // Set Amplitude and Frequency
        perlin.m_AmplitudeGain = amp;
        perlin.m_FrequencyGain = freq;
    }
}