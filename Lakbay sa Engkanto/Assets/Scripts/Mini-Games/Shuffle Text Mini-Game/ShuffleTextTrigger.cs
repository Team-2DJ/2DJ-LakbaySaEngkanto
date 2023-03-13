using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ShuffleTextTrigger : MonoBehaviour
{
    [Header("Shuffle Text Word Data")]
    [SerializeField] private ShuffleTextData shuffleTextData;
    
    private bool isComplete;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isComplete) return;

        SingletonManager.Get<UIEvents>().AddButtonListener(EnableShuffleTextMiniGame);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isComplete) return;

        SingletonManager.Get<UIEvents>().RemoveButtonListener(EnableShuffleTextMiniGame);
    }

    private void EnableShuffleTextMiniGame()
    {
        SingletonManager.Get<PlayerEvents>().SetPlayerMovement(false);
        SingletonManager.Get<PanelManager>().ActivatePanel("Shuffle Text Panel", 1f);
        SingletonManager.Get<ShuffleTextMiniGame>().Initialize(shuffleTextData, this);
    }

    public void Completed()
    {
        isComplete = true;
    }
}
