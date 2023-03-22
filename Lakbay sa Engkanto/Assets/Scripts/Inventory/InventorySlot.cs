using UnityEngine.EventSystems;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public RectTransform rectTransform { get; private set; }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

}
