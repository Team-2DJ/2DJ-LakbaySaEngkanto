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

    private bool isTransitioning;                                                       // Indicates if Panel is Undergoing DoTween Animation

    #region Singleton
    private void Awake()
    {
        SingletonManager.Register(this);
    }
    #endregion

    public void Initialize(Sprite[] panels)
    {
        currentIndex = 0;
        panelSprites = panels;
        isTransitioning = false;

        // Start
        NextPanel();
    }

    /// <summary>
    /// Proceed to the Next Panel
    /// </summary>
    public void NextPanel()
    {
        if (isTransitioning)
            return;
        
        // If Current Index has Reached Maximum Amount of Panels
        if (currentIndex > panelSprites.Length - 1)
        {
            // Clamp Current Index to the Max Length of Panel Sprites
            currentIndex = panelSprites.Length - 1;

            CanvasGroup canvas = panelImage.gameObject.GetComponent<CanvasGroup>();

            canvas.alpha = 1f;
            canvas.DOFade(0, 1f).OnComplete(() => EndCutscene());
        }
        else
        {
            // Set Panel Image Based on Current Index
            SetPanelImage(currentIndex);

            // Increment Current Index After Activation
            currentIndex++;
        }
    }

    /// <summary>
    /// Set Sprite of Panel Image
    /// </summary>
    /// <param name="index"></param>
    public void SetPanelImage(int index)
    {
        panelImage.sprite = panelSprites[index];

        isTransitioning = true; 
        CanvasGroup canvas = panelImage.gameObject.GetComponent<CanvasGroup>();

        canvas.alpha = 0f;
        canvas.DOFade(1, 1f).OnComplete(() => isTransitioning = false);
    }

    void EndCutscene()
    {
        // Return Back to Game Panel
        SingletonManager.Get<PanelManager>().ActivatePanel("Game Panel");

        // Enable Player Movement
        SingletonManager.Get<PlayerEvents>().SetPlayerMovement(true);
    }
}
