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

    private float playerHp;

    void Start()
    {
        SingletonManager.Get<GameEvents>().OnUpdateUI += UpdateHealth;
    }

    void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnUpdateUI -= UpdateHealth;
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
}
