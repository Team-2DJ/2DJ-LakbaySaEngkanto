using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class PodiumBook : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    private Collider2D playerCollider;

    void Start()
    {
        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            Destroy(gameObject);
            SingletonManager.Get<PlayerManager>().PlayerInventory.AddItem(itemData);
        }
    }
}
