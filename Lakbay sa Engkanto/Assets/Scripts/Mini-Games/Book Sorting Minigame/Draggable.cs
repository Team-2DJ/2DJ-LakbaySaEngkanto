using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Draggable : MonoBehaviour
{
    private Vector2 originalPosition, mouseOffset;
    private Camera cameraReference;

    private void Awake()
    {
        originalPosition = this.transform.position;
        cameraReference = Camera.main;
    }

    private void OnMouseDrag()
    {
        Debug.Log("Dragging");
        transform.position = GetMouseWorldPos() + mouseOffset;
    }

    private void OnMouseDown()
    {
        Debug.Log("mouseDown");
        mouseOffset = (Vector2)gameObject.transform.position - GetMouseWorldPos();
    }

    private void OnMouseUp()
    {
        Debug.Log("mouseUp");
        transform.position = originalPosition;
    }

    Vector2 GetMouseWorldPos()
    {
        return (Vector2)cameraReference.ScreenToWorldPoint(Input.mousePosition);
    }
}
