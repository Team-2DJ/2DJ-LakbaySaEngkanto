using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class Blocker : MonoBehaviour
{
    [SerializeField] private string id;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;


    private void OnEnable()
    {
        SingletonManager.Get<GameEvents>().OnCloseDoor += CloseArea;
    }

    private void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnCloseDoor -= CloseArea;
    }

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (SingletonManager.Get<PlayerManager>().PlayerData.StringList.Contains(id))
        {
            CloseArea(id);
            return;
        }

        boxCollider2D.enabled = false;
        spriteRenderer.enabled = false;
    }

    void CloseArea(string id)
    {
        if (id != this.id) return;

        boxCollider2D.enabled = true;
        spriteRenderer.enabled = true;
    }
}
