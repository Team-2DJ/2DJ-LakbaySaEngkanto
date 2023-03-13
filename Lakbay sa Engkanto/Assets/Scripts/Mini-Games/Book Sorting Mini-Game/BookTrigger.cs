using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BookTrigger : MonoBehaviour
{
    [SerializeField] private BooksMiniGame booksMiniGame;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (booksMiniGame.IsComplete) return;

        SingletonManager.Get<UIEvents>().AddButtonListener(EnableBooksMiniGame);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (booksMiniGame.IsComplete) return;

        SingletonManager.Get<UIEvents>().RemoveButtonListener(EnableBooksMiniGame);
        booksMiniGame?.gameObject.SetActive(false);
    }

    private void EnableBooksMiniGame()
    {
        booksMiniGame?.gameObject.SetActive(true);
    }
}
