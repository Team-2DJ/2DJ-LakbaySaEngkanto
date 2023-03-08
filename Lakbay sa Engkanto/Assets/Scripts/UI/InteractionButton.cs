using UnityEngine;
using UnityEngine.UI;
using System;

public class InteractionButton : MonoBehaviour
{
    [SerializeField] private Button button;

    private void OnEnable()
    {
        SingletonManager.Get<GameEvents>().OnAddActionListener += AddButtonListener;
        SingletonManager.Get<GameEvents>().OnRemoveActionListener += RemoveButtonListener;
    }

    private void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnAddActionListener -= AddButtonListener;
        SingletonManager.Get<GameEvents>().OnRemoveActionListener -= RemoveButtonListener;
    }

    private void AddButtonListener(Action actionVoid)
    {
        button.onClick.AddListener(() => actionVoid());
        button.gameObject.SetActive(true);
    }

    private void RemoveButtonListener(Action actionVoid)
    {
        button.onClick.RemoveAllListeners();
        button.gameObject.SetActive(false);
    }
}
