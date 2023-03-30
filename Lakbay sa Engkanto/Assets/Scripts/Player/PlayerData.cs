using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // Get Access to all trigger gameobjects here

    public List<string> StringList { get; private set; } = new();

    public List<string> ShuffleTriggerList { get; private set; } = new();

    public List<string> KapreList { get; private set; } = new();

    public bool HasMetChichay { get; set; }

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

    public void ClearData()
    {
        StringList.Clear();
        ShuffleTriggerList.Clear();
        HasMetChichay = false;
    }
}
