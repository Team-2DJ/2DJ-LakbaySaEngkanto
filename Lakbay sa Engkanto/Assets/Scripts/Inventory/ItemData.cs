using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    PodiumBook
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string displayName;
    [SerializeField] private ItemType typeOfItem;
    [SerializeField] private Sprite icon;

    public Sprite GetIcon() { return icon; }
    public ItemType GetTypeOfItem() { return typeOfItem; }
    public string GetDisplayName() { return displayName; }
}
