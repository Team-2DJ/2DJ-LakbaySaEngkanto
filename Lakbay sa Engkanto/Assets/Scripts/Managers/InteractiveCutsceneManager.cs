using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InteractiveCutsceneManager : MonoBehaviour
{
    [Header("References")]    
    [SerializeField] private Image panelImage;                                          // Panel Image Reference

    private int currentIndex;                                                           // Current Panel Index
    private Sprite[] panelSprites;                                                      // Panel Sprite Array

    private bool isTransitioning;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    public void Initialize(Sprite[] panels)
    {
        currentIndex = 0;
        panelSprites = panels;
        isTransitioning = false;

        NextPanel();
    }

    public void NextPanel()
    {
        if (isTransitioning)
            return;
        
        if (currentIndex > panelSprites.Length - 1)
        {
            currentIndex = panelSprites.Length - 1;
            SingletonManager.Get<PanelManager>().ActivatePanel("Game Panel", 0f);

            // Enable Player Movement
            SingletonManager.Get<PlayerEvents>().SetPlayerMovement(true);
        }
        else
        {
            ActivatePanel(currentIndex);
            currentIndex++;
        }
    }

    
    public void ActivatePanel(int index)
    {
        panelImage.sprite = panelSprites[index];

        isTransitioning = true; 
        CanvasGroup canvas = panelImage.gameObject.GetComponent<CanvasGroup>();

        canvas.alpha = 0f;
        canvas.DOFade(1, 1f).OnComplete(() => isTransitioning = false);
    }
}
