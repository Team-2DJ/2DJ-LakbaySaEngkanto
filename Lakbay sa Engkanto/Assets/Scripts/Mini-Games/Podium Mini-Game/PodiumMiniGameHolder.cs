using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodiumMiniGameHolder : MonoBehaviour
{
    [SerializeField] private List<PodiumMiniGame> podiumSortingMiniGames = new();
    private Dictionary<string, PodiumMiniGame> podiumMiniGameDictionary = new();

    private void OnEnable()
    {
        SingletonManager.Get<UIEvents>().OnActivatePanel += ActivateMiniGame;
        Initialize();
    }

    private void OnDisable()
    {
        SingletonManager.Get<UIEvents>().OnActivatePanel -= ActivateMiniGame;
    }

    private void Initialize()
    {
        foreach (PodiumMiniGame podiumMiniGame in podiumSortingMiniGames)
        {
            podiumMiniGame.gameObject.SetActive(false);

            podiumMiniGameDictionary.TryAdd(podiumMiniGame.GetID(), podiumMiniGame);
        }
    }

    private void ActivateMiniGame(string id)
    {
        DeactivateAllSortingGames();

        if (podiumMiniGameDictionary[id].IsComplete) return;
        podiumMiniGameDictionary[id]?.gameObject.SetActive(true);
    }

    private void DeactivateAllSortingGames()
    {
        foreach (PodiumMiniGame miniGame in podiumMiniGameDictionary.Values)
        {
            miniGame.gameObject.SetActive(false);
        }
    }

    private void Reset()
    {
        PodiumMiniGame[] temp = GetComponentsInChildren<PodiumMiniGame>();

        foreach (PodiumMiniGame miniGame in temp)
        {
            podiumSortingMiniGames.Add(miniGame);
        }
    }
}
