using UnityEngine;

public class BookTrigger : MonoBehaviour
{
    [SerializeField] private BooksMiniGame booksMiniGame;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (booksMiniGame.IsComplete) return;

        SingletonManager.Get<GameEvents>().AddActionListener(EnableBooksMiniGame);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (booksMiniGame.IsComplete) return;

        SingletonManager.Get<GameEvents>().RemoveActionListener(EnableBooksMiniGame);
        booksMiniGame?.gameObject.SetActive(false);
    }

    private void EnableBooksMiniGame()
    {
        booksMiniGame?.gameObject.SetActive(true);
    }
}
