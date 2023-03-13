using UnityEngine;
using System;

/// <summary>
/// Manages all UI Events
/// </summary>
public class UIEvents : MonoBehaviour
{
    public event Action<Action> OnAddButtonListener, OnRemoveButtonListener;          // Used to add listners programatically to buttons
    public event Action<string> OnActivatePanel;

    void Awake()
    {
        SingletonManager.Register(this);
    }

    public void AddButtonListener(Action action)
    {
        OnAddButtonListener.Invoke(action);
    }

    public void RemoveButtonListener(Action action)
    {
        OnRemoveButtonListener.Invoke(action);
    }

    public void ActivatePanel(string id)
    {
        OnActivatePanel.Invoke(id);
    }
}
