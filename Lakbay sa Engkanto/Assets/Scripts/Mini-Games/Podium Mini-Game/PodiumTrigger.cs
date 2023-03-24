using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PodiumTrigger : MonoBehaviour
{
    [SerializeField] private string panelToActivate;
    private bool hasRightBook;

    private void OnEnable()
    {
        SingletonManager.Get<GameEvents>().OnSetCondition += GameCompleted;
    }

    private void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnSetCondition -= GameCompleted;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasRightBook) return;

        SingletonManager.Get<UIEvents>().AddButtonListener(EnableBooksMiniGame);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (hasRightBook) return;

        SingletonManager.Get<UIEvents>().RemoveButtonListener(EnableBooksMiniGame);
    }

    private void EnableBooksMiniGame()
    {
        SingletonManager.Get<UIEvents>().ActivatePanel(panelToActivate);
        SingletonManager.Get<PlayerEvents>().SetPlayerMovement(true);
    }

    private void GameCompleted(string id, bool condition)
    {
        if (id != panelToActivate) return;

        hasRightBook = condition;
    }
}
