using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class CollectibleBook : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    private Collider2D playerCollider;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = itemData.GetClosedIcon();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            SingletonManager.Get<PlayerManager>().PlayerInventory.AddItem(itemData);

            Destroy(gameObject);
        }
    }
}
