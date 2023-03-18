using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class BookTrigger : MonoBehaviour
{
    [SerializeField] private string gameToActivate;
    [SerializeField] private Sprite sorted, notSorted;
    private bool isGameComplete;
    private SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        SingletonManager.Get<GameEvents>().OnSetCondition += GameCompleted;
    }

    private void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnSetCondition -= GameCompleted;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = notSorted;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isGameComplete) return;

        SingletonManager.Get<UIEvents>().AddButtonListener(EnableBooksMiniGame);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isGameComplete) return;

        SingletonManager.Get<UIEvents>().RemoveButtonListener(EnableBooksMiniGame);
    }

    private void EnableBooksMiniGame()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Book Sorting Panel");
        SingletonManager.Get<UIEvents>().ActivatePanel(gameToActivate);
    }

    private void GameCompleted(string id, bool condition)
    {
        if (id != gameToActivate) return;
        isGameComplete = condition;

        spriteRenderer.sprite = isGameComplete ? sorted : notSorted;
    }
}
