using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Controls In-Game UI
/// </summary>
public class UIController : MonoBehaviour
{
    [SerializeField] Image[] PlayerHearts;
    [SerializeField] Sprite FullHeartContainer;
    [SerializeField] Sprite EmptyHeartContainer;

    private float playerHp;

    void Start()
    {
        SingletonManager.Get<GameEvents>().OnPlayerDamaged += UpdateHealth;
    }

    void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnPlayerDamaged -= UpdateHealth;
    }

    // Update Player Health Container
    public void UpdateHealth(float hp)
    {
        Debug.Log("Update HP");

        hp = SingletonManager.Get<PlayerManager>().Player.GetComponent<HealthComponent>().CurrentHealth;

        for (int i = 0; i < PlayerHearts.Length; i++)
        {
            if (i < hp)
            {
                PlayerHearts[i].sprite = FullHeartContainer;
            }
            else
            {
                PlayerHearts[i].sprite = EmptyHeartContainer;
            }
        }
    }
}
