using UnityEngine;
using System;

/// <summary>
/// Manages all UI Events
/// </summary>
public class UIEvents : MonoBehaviour
{
    // Interact Button Events
    public event Action<Action> OnAddButtonListener, OnRemoveButtonListener;          // Used to add listners programatically to buttons
    public event Action<string> OnActivatePanel;
    public event Action<string, bool> OnActivateButton;

    void Awake()
    {
        SingletonManager.Register(this);
    }

    public void ActivateButton(string id, bool condition)
    {
        OnActivateButton?.Invoke(id, condition);
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
