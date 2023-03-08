using UnityEngine;

public class BookTrigger : MonoBehaviour
{
    [SerializeField] private GameObject booksMiniGame;

    private void OnTriggerEnter2D(Collider2D other)
    {
        SingletonManager.Get<GameEvents>().AddActionListener(EnableBooksMiniGame);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        SingletonManager.Get<GameEvents>().RemoveActionListener(EnableBooksMiniGame);
        booksMiniGame?.SetActive(false);
    }

    private void EnableBooksMiniGame()
    {
        booksMiniGame?.SetActive(true);
    }
}
