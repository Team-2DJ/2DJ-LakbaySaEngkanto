using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class EnableJournal : MonoBehaviour
{
    [SerializeField] private Button journalButton;
    [SerializeField] private bool enableAtStart;

    private void OnEnable()
    {
        SingletonManager.Get<UIEvents>().OnActivateButton += SetButton;
    }

    private void OnDisable()
    {
        SingletonManager.Get<UIEvents>().OnActivateButton -= SetButton;
    }

    private void Awake()
    {
        SetButton(enableAtStart);
    }

    private void Start()
    {
        SetButton(SingletonManager.Get<PlayerManager>().PlayerData.JournalActivated);
    }

    private void SetButton(bool condition)
    {
        journalButton.gameObject.SetActive(condition);
    }
}
