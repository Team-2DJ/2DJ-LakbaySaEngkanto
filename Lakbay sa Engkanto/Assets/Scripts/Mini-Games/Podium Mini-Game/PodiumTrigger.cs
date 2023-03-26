using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PodiumTrigger : MonoBehaviour
{
    [SerializeField] private string gameToActivate;
    [SerializeField] private Sprite completed, notCompleted;
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
        spriteRenderer.sprite = notCompleted;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isGameComplete) return;

        SingletonManager.Get<UIEvents>().AddButtonListener(EnablePodiumMiniGame);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isGameComplete) return;

        SingletonManager.Get<UIEvents>().RemoveButtonListener(EnablePodiumMiniGame);
    }

    private void EnablePodiumMiniGame()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Podium Panel");
        SingletonManager.Get<UIEvents>().ActivatePanel(gameToActivate);
        SingletonManager.Get<PlayerEvents>().SetPlayerMovement(false);
    }

    private void GameCompleted(string id, bool condition)
    {
        if (id != gameToActivate) return;
        isGameComplete = condition;

        spriteRenderer.sprite = isGameComplete ? completed : notCompleted;
    }
}
