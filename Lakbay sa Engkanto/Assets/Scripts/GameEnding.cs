using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameEnding : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image panelImage;                                          // Panel Image Reference
    [SerializeField] private Animator arrowAnimator;

    [Header("Properties")]
    [SerializeField] private Sprite[] panelSprites;                                     // Panel Sprite Array

    private int currentIndex;                                                           // Current Panel Index
    private CanvasGroup canvas;                                                         // PanelImage Canvas Group Component Reference
    private bool isTransitioning;                                                       // Indicates if Panel is Undergoing DoTween Animation

    // Start is called before the first frame update
    void Start()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Ending Cutscene");
        SingletonManager.Get<AudioManager>().Play("Ending BGM");

        Initialize(panelSprites);
    }

    public void Initialize(Sprite[] panels)
    {
        if (canvas == null)
            canvas = panelImage.gameObject.GetComponent<CanvasGroup>();

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

            isTransitioning = true;

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
        arrowAnimator.SetBool("isFinished", false);

        panelImage.sprite = panelSprites[index];

        isTransitioning = true;

        canvas.alpha = 0f;
        canvas.DOFade(1, 1f).OnComplete(OnPanelComplete);
    }

    /// <summary>
    /// Indicates if Panel has Finished Transition and Has been Set
    /// </summary>
    void OnPanelComplete()
    {
        isTransitioning = false;

        arrowAnimator.SetBool("isFinished", true);
    }

    /// <summary>
    /// End the Cutscene then Go back to Main Menu
    /// </summary>
    void EndCutscene()
    {
        // Declare Game as Finished
        SingletonManager.Get<PlayerManager>().PlayerData.GameIsFinished = true;

        string[] scenes = { "MainMenuScene" };

        SingletonManager.Get<SceneLoader>().LoadScene(scenes);
    }
}
