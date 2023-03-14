using System.Collections.Generic;
using UnityEngine;

public class BooksMiniGameHolder : MonoBehaviour
{
    [SerializeField] private List<BookSortingMiniGame> bookSortingMiniGames = new();
    private Dictionary<string, BookSortingMiniGame> booksMiniGameDictionary = new();

    private void OnEnable()
    {
        SingletonManager.Get<UIEvents>().OnActivatePanel += ActivateMiniGame;
    }

    private void OnDisable()
    {
        SingletonManager.Get<UIEvents>().OnActivatePanel -= ActivateMiniGame;
    }

    private void Start()
    {
        foreach (BookSortingMiniGame bookMiniGame in bookSortingMiniGames)
        {
            bookMiniGame.gameObject.SetActive(false);

            booksMiniGameDictionary.TryAdd(bookMiniGame.GetID(), bookMiniGame);
        }
    }

    private void ActivateMiniGame(string id)
    {
        DeactivateAllSortingGames();

        if (booksMiniGameDictionary[id].IsComplete) return;
        booksMiniGameDictionary[id]?.gameObject.SetActive(true);
    }

    private void DeactivateAllSortingGames()
    {
        foreach (BookSortingMiniGame miniGame in booksMiniGameDictionary.Values)
        {
            miniGame.gameObject.SetActive(false);
        }
    }

    private void Reset()
    {
        BookSortingMiniGame[] temp = GetComponentsInChildren<BookSortingMiniGame>();

        foreach (BookSortingMiniGame miniGame in temp)
        {
            bookSortingMiniGames.Add(miniGame);
        }
    }
}
