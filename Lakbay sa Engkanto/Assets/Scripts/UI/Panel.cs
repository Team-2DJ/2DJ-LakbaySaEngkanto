using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class Panel : MonoBehaviour
{
    [SerializeField] private float transitionDuration;                      // Fade-In Duration

    private CanvasGroup canvasGroup;                                        // Canvas Group Component Reference
    private Sequence sequence;                                              // DoTween Animation Holder

    private void OnEnable()
    {
        FadeIn();
    }

    private void OnDisable()
    {
        // Cancel Do-Tween Animation
        sequence.Kill();
    }

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// Gives a Fade-In Animation to the Panel
    /// </summary>
    public void FadeIn()
    {
        // Set Panel to Transparent
        canvasGroup.alpha = 0f;

        // Initialize DoTween Sequence
        sequence = DOTween.Sequence();

        // Store Fade Animation in Sequence Holder,
        // Then Execute Animation
        sequence.Append(canvasGroup.DOFade(1f, transitionDuration));
    }
}
