using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BookSlot : MonoBehaviour
{
    public SpriteRenderer Renderer { get; private set; }

    void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
    }



}
