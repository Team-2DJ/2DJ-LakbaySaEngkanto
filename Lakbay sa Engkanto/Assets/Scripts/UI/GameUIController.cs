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
    [SerializeField] private GameObject JournalButtonReference;
    [SerializeField] private string id;

    private void OnEnable()
    {
        // NOTE: MIGHT BE MOVED IN THE FUTURE
        SingletonManager.Get<PlayerEvents>().OnPlayerCollectItem += JournalButtonActivate;
    }

    private void OnDisable()
    {
        // NOTE: MIGHT BE MOVED IN THE FUTURE
        SingletonManager.Get<PlayerEvents>().OnPlayerCollectItem -= JournalButtonActivate;
    }

    void Start()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Game Panel");
        //JournalButtonReference.SetActive(false);
    }

    // TO BE FIXED!!!
    public void OnJournalButtonClicked()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Journal Panel");
        
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

    public void OnMainMenuButtonClicked()
    {
        Time.timeScale = 1f;

        string[] scenes = { "MainMenuScene" };

        SingletonManager.Get<SceneLoader>().LoadScene(scenes);
    }

    public void JournalButtonActivate(string id)
    {
        if (id != this.id)
            return;

        JournalButtonReference.SetActive(true);
    }
}
