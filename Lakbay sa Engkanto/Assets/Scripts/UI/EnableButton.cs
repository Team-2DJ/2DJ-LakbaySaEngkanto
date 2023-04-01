using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class EnableButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private bool enableAtStart;

    private void OnEnable()
    {
        SingletonManager.Get<UIEvents>().OnActivateButton += SetButton;
    }

    private void OnDisable()
    {
        SingletonManager.Get<UIEvents>().OnActivateButton -= SetButton;
    }

    private void Start()
    {
        SetButton(enableAtStart);
    }

    private void SetButton(bool condition)
    {
        button.gameObject.SetActive(condition);
    }
}
