using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // Get Access to all trigger gameobjects here
    public Dictionary<string, PodiumMiniGame> PodiumDictionary { get; private set; } = new();
    public Dictionary<string, Cutscenes> CutscenesDictionary { get; private set; } = new();
    public Dictionary<string, CollectibleBook> CollectiblesDictionary { get; private set; } = new();


    public void AddData(string key, PodiumMiniGame miniGame)
    {
        PodiumDictionary.TryAdd(key, miniGame);
    }

    public void AddData(string key, Cutscenes cutscene)
    {
        CutscenesDictionary.TryAdd(key, cutscene);
    }

    public void AddData(string key, CollectibleBook collectible)
    {
        CollectiblesDictionary.TryAdd(key, collectible);
    }

    public void RemoveData(string key, PodiumMiniGame miniGame)
    {
        if (PodiumDictionary.ContainsKey(key))
        {
            PodiumDictionary.Remove(key);
        }
    }
    public void RemoveData(string key, Cutscenes cutscene)
    {
        if (CutscenesDictionary.ContainsKey(key))
        {
            CutscenesDictionary.Remove(key);
        }
    }

    public void RemoveData(string key, CollectibleBook book)
    {
        if (CollectiblesDictionary.ContainsKey(key))
        {
            CollectiblesDictionary.Remove(key);
        }
    }
}
