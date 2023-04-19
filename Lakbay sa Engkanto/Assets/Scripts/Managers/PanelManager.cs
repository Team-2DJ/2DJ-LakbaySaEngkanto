using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    [SerializeField] private PanelData[] panels;

    #region Singleton
    void Awake()
    {
        SingletonManager.Register(this);
    }
    #endregion

    /// <summary>
    /// Activates Selected Panel Based on ID Input
    /// </summary>
    /// <param name="id"></param>
    public void ActivatePanel(string id)
    {
        // Enable Chosen Panel and disable the rest
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].PanelObject.SetActive(panels[i].Id == id);
        }
    }
}
