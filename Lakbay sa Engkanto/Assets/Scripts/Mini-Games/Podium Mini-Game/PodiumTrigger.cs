using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PodiumTrigger : MonoBehaviour
{
    [SerializeField] private string id;
    [SerializeField] private Sprite completed, notCompleted;

    [Header("Gameplay Settings")]
    [SerializeField] private string doorToOpen;                         // Door To Open string
    [TextArea(3, 10)]
    [SerializeField] private string question;                           // The Question that will be shown
    [SerializeField] private GameObject page;                           // Journal Page to Instatiate
    [SerializeField] private ItemData itemData;                         // itemData that will be used for checking


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

        if (SingletonManager.Get<PlayerManager>().PlayerData.StringList.Contains(id))
        {
            GameCompleted(id, true);
            SingletonManager.Get<GameEvents>().OpenDoor(doorToOpen);
            return;
        }
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
        SingletonManager.Get<PlayerEvents>().SetPlayerMovement(false);

        SingletonManager.Get<PodiumMiniGame>().Initialize(id, doorToOpen, question, page, itemData);
    }

    private void GameCompleted(string id, bool condition)
    {
        if (id != this.id) return;
        isGameComplete = condition;

        if (!isGameComplete) return;

        SingletonManager.Get<PlayerManager>().PlayerData.AddString(id);
        spriteRenderer.sprite = isGameComplete ? completed : notCompleted;

    }
}
