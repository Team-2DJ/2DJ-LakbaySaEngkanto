using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string displayName;
    [SerializeField] private Sprite icon;
}
