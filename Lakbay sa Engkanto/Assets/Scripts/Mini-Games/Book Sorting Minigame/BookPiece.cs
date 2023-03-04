using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPiece : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private bool dragging, placed;
    private Camera cameraReference;

    private Vector2 offset, originalPosition;

    private BookSlot bookSlot;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cameraReference = Camera.main;
    }

    private void Start()
    {
        originalPosition = transform.position;
    }

    public void Initialize(BookSlot bookSlot)
    {
        spriteRenderer.sprite = bookSlot.Renderer.sprite;
        this.bookSlot = bookSlot;
    }

    private void Update()
    {
        if (placed) return;
        if (!dragging) return;

        var mousePosition = GetMousePos();
        transform.position = mousePosition - offset;
    }

    private void OnMouseDown()
    {
        dragging = true;

        offset = GetMousePos() - (Vector2)transform.position;
    }

    private void OnMouseUp()
    {
        dragging = false;

        if (Vector2.Distance(transform.position, bookSlot.transform.position) < 3)
        {
            transform.position = bookSlot.transform.position;
            placed = true;
        }
        else
        {
            transform.position = originalPosition;
        }
    }

    Vector2 GetMousePos()
    {
        return cameraReference.ScreenToWorldPoint(Input.mousePosition);
    }
}
