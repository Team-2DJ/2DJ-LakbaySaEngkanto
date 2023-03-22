using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PodiumBook : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    private Collider2D playerCollider;

    [SerializeField] bool isAdd;

    void Start()
    {
        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            if (isAdd)
                SingletonManager.Get<PlayerManager>().PlayerInventory.AddItem(itemData);
            else
                SingletonManager.Get<PlayerManager>().PlayerInventory.RemoveItem(itemData);

            Destroy(gameObject);
        }
    }
}
