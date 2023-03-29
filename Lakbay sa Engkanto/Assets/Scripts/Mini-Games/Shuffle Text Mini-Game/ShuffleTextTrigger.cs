using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ShuffleTextTrigger : MonoBehaviour
{
    [Header("Shuffle Text Word Data")]
    [SerializeField] private ShuffleTextData shuffleTextData;

    [Header("Properties")]
    [SerializeField] private string doorId;                                     // Door To Open string

    private bool isComplete;
    private Animator animator;
    private Collider2D playerCollider;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();

        // Check if Mini-Game has been Completed by Player
        //SingletonManager.Get<GameEvents>().OpenDoor(doorId);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isComplete) return;

        if (other == playerCollider)
            SingletonManager.Get<UIEvents>().AddButtonListener(EnableShuffleTextMiniGame);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isComplete) return;

        if (other == playerCollider)
            SingletonManager.Get<UIEvents>().RemoveButtonListener(EnableShuffleTextMiniGame);
    }

    private void EnableShuffleTextMiniGame()
    {
        SingletonManager.Get<PlayerEvents>().SetPlayerMovement(false);
        SingletonManager.Get<PanelManager>().ActivatePanel("Shuffle Text Panel");
        SingletonManager.Get<ShuffleTextMiniGame>().Initialize(shuffleTextData, this, doorId);
    }

    public void Completed()
    {
        isComplete = true;
        animator.SetBool("isComplete", isComplete);
    }
}
