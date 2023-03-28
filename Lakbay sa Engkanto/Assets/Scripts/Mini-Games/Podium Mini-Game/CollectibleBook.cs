using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class CollectibleBook : MonoBehaviour
{
    [SerializeField] private string id;                              // Object ID 
    [SerializeField] private ItemData itemData;                      // inventory item Data
    private Collider2D playerCollider;                               // player Collider
    private SpriteRenderer spriteRenderer;                           // this Sprite Renderer

    void OnEnable()
    {
        if (SingletonManager.Get<PlayerManager>().PlayerInventory.ItemDataList.Contains(itemData))
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = itemData.GetClosedIcon();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the object that collides is a playerCollider
        if (other == playerCollider)
        {
            // Adds the Item to the PlayerInventory using itemData as its parameter
            SingletonManager.Get<PlayerManager>().PlayerInventory.AddItem(itemData);

            Destroy(gameObject);
        }
    }
}
