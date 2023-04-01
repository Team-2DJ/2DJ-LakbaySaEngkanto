using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Controls In-Game UI
/// </summary>
public class GameUIController : MonoBehaviour
{
    void Start()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Game Panel");
    }

    public void OnJournalButtonClicked()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Journal Panel");
        Time.timeScale = 0f;
    }

    public void OnPauseButtonClicked()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Pause Panel");
        Time.timeScale = 0f;
    }

    public void OnResumeButtonClicked()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Game Panel");
        Time.timeScale = 1f;
    }

    public void OnDebugModeButtonClicked()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Debug Panel");
        Time.timeScale = 1f;
    }

    public void OnInteractButtonClicked()
    {
        // Play Interact SFX
        SingletonManager.Get<AudioManager>().PlayOneShot("Interact");
    }

    public void OnMainMenuButtonClicked()
    {
        Time.timeScale = 1f;

        string[] scenes = { "MainMenuScene" };

        SingletonManager.Get<SceneLoader>().LoadScene(scenes);
    }
}
