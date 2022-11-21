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
    [SerializeField] Image[] PlayerHearts;
    [SerializeField] Sprite FullHeartContainer;
    [SerializeField] Sprite EmptyHeartContainer;
    [SerializeField] GameObject JournalButtonReference; 

    private float playerHp;

    void Start()
    {
        SingletonManager.Get<GameEvents>().OnUpdateUI += UpdateHealth;
        SingletonManager.Get<GameEvents>().OnPickupPage += JournalButtonActivate;
        JournalButtonReference.SetActive(false);
    }

    void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnUpdateUI -= UpdateHealth;
        SingletonManager.Get<GameEvents>().OnPickupPage -= JournalButtonActivate;
    }

    // Update Player Health Container
    public void UpdateHealth()
    {
        playerHp = SingletonManager.Get<PlayerManager>().Player.GetComponent<HealthComponent>().CurrentHealth;

        for (int i = 0; i < PlayerHearts.Length; i++)
        {
            if (i < playerHp)
            {
                PlayerHearts[i].sprite = FullHeartContainer;
            }
            else
            {
                PlayerHearts[i].sprite = EmptyHeartContainer;
            }
        }
    }

    public void OnJournalButtonClicked()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("journal-panel");
        Time.timeScale = 0f;
    }

    public void OnPauseButtonClicked()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("pause-panel");
        Time.timeScale = 0f;
    }

    public void OnResumeButtonClicked()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("game-panel");
        Time.timeScale = 1f;
    }

    public void JournalButtonActivate()
    {
        JournalButtonReference.SetActive(true);
    }
}
