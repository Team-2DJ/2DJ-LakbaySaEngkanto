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
    [SerializeField] private Image[] PlayerHearts;
    [SerializeField] private Sprite FullHeartContainer;
    [SerializeField] private Sprite EmptyHeartContainer;
    [SerializeField] private GameObject JournalButtonReference;
    [SerializeField] private string id;

    private float playerHp;

    private void OnEnable()
    {
        SingletonManager.Get<GameEvents>().OnUpdateUI += UpdateHealth;
        SingletonManager.Get<GameEvents>().OnPlayerCollectItem += JournalButtonActivate;
    }

    private void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnUpdateUI -= UpdateHealth;
        SingletonManager.Get<GameEvents>().OnPlayerCollectItem -= JournalButtonActivate;
    }

    void Start()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Game Panel", 0f);
        JournalButtonReference.SetActive(false);
    }

    // Update Player Health Container
    public void UpdateHealth()
    {
        playerHp = SingletonManager.Get<PlayerManager>().Player.GetComponent<HealthComponent>().CurrentHealth;

        for (int i = 0; i < PlayerHearts.Length; i++)
        {
            if (i < playerHp)
                PlayerHearts[i].sprite = FullHeartContainer;
            else
                PlayerHearts[i].sprite = EmptyHeartContainer;
        }
    }

    // TO BE FIXED!!!
    public void OnJournalButtonClicked()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Journal Panel", 0f);
        Time.timeScale = 0f;
    }

    public void OnPauseButtonClicked()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Pause Panel", 0f);
        Time.timeScale = 0f;
    }

    public void OnResumeButtonClicked()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Game Panel", 0f);
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
