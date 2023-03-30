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

    private void OnEnable()
    {
        if (SingletonManager.Get<PlayerManager>().PlayerData.StringList.Contains(doorId))
        {
            isComplete = true;
            animator.SetBool("isComplete", isComplete);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();

        if (SingletonManager.Get<PlayerManager>().PlayerData.StringList.Contains(doorId))
        {
            isComplete = true;
            animator.SetBool("isComplete", isComplete);
            SingletonManager.Get<GameEvents>().OpenDoor(doorId);
        }
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
        SingletonManager.Get<PlayerManager>().PlayerData.AddString(doorId);
        
        isComplete = true;
        animator.SetBool("isComplete", isComplete);
    }
}
