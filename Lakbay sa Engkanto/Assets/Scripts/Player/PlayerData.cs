using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // Get Access to all trigger gameobjects here

    public List<string> StringList { get; private set; } = new();

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
}
