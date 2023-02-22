using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private GameObject transitionPanel;

    private CanvasGroup transitionPanelGroup;

    private void Start()
    {
        transitionPanelGroup = transitionPanel.GetComponent<CanvasGroup>();
    }

    public void StartLoading()
    {
        transitionPanel.SetActive(false);
        loadingPanel.SetActive(true);
    }

    public void EndLoading()
    {
        loadingPanel.SetActive(false);

        transitionPanel.SetActive(true);

        // Trigger Fade-Out Animation
        transitionPanelGroup.alpha = 1f;
        transitionPanelGroup.DOFade(0, 1f).OnComplete(() => transitionPanel.SetActive(false));
    }
}
