using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class EnableJournal : MonoBehaviour
{
    private string id = "EnableJournal";
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
        SetButton(id, enableAtStart);
    }

    private void Start()
    {
        SetButton(id, SingletonManager.Get<PlayerManager>().PlayerData.JournalActivated);
    }

    private void SetButton(string id, bool condition)
    {
        if (id != this.id) return;
        journalButton.gameObject.SetActive(condition);
    }
}
