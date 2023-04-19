using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    private string id = "TutorialButton";
    [SerializeField] private TutorialData[] tutorialDatas;

    private Collider2D playerCollider;

    private void Start()
    {
        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            SingletonManager.Get<TutorialScreen>().Initialize(tutorialDatas);
            SingletonManager.Get<UIEvents>().ActivateButton(id, true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            SingletonManager.Get<UIEvents>().ActivateButton(id, false);
        }
    }

}
