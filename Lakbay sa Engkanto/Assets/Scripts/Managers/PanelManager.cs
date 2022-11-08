using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages UI Panels
/// </summary>
public class PanelManager : MonoBehaviour
{
    [System.Serializable]
    // Panel Data Struct
    public struct PanelData
    {
        public string Id;
        public GameObject PanelObject;
    }

    [Header("References")]
    public PanelData[] Panels;

    #region Singleton
    void Awake()
    {
        SingletonManager.Register(this);
    }
    #endregion

    /// <summary>
    /// Activates Selected Panel Based in ID Input
    /// </summary>
    /// <param name="id"></param>
    public void ActivatePanel(string id)
    {
        // Enable Chosen Panel and disable the rest
        for (int i = 0; i < Panels.Length; i++)
        {
            Panels[i].PanelObject.SetActive(Panels[i].Id == id);
        }
    }
}
