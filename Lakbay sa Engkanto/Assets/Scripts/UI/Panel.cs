using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class Panel : MonoBehaviour
{
    [SerializeField] private float transitionDuration;

    private CanvasGroup canvasGroup;
    private Sequence mySequence;

    private void OnEnable()
    {
        FadeIn();
    }

    private void OnDisable()
    {
        // Cancel Do-Tween Animation
        mySequence.Kill();
    }

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void FadeIn()
    {
        canvasGroup.alpha = 0f;

        mySequence = DOTween.Sequence();
        mySequence.Append(canvasGroup.DOFade(1f, transitionDuration));
    }
}
