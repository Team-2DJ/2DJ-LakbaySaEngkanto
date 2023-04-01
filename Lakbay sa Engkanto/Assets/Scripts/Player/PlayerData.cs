using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // Get Access to all trigger gameobjects here

    public List<string> StringList { get; private set; } = new();

    public List<ItemData> ItemDataList { get; private set; } = new();

    public int PagesCollected { get; private set; }

    public bool JournalActivated { get; private set; }

    public bool HasMetChichay { get; set; }

    void Start()
    {
        ClearData();
    }

    public void AddPagesCollected(int value)
    {
        PagesCollected += value;

        JournalActivated = PagesCollected >= 0;
    }

    public void AddString(string value)
    {
        if (StringList.Contains(value)) return;

        StringList.Add(value);
    }

    public void RemoveString(string value)
    {
        if (!StringList.Contains(value)) return;

        StringList.Remove(value);
    }

    public void AddItemData(ItemData value)
    {
        if (ItemDataList.Contains(value)) return;

        ItemDataList.Add(value);
    }

    public void RemoveItemData(ItemData value)
    {
        if (!ItemDataList.Contains(value)) return;

        ItemDataList.Remove(value);
    }

    public void ClearData()
    {
        StringList.Clear();
        ItemDataList.Clear();
        PagesCollected = -1;
        JournalActivated = false;
        HasMetChichay = false;
    }
}
