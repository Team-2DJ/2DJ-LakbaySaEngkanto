using UnityEngine.UI;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    private string id = "TutorialButton";
    [SerializeField] private Button button;

    private void OnEnable()
    {
        SingletonManager.Get<UIEvents>().OnActivateButton += EnableButton;
    }

    private void OnDisable()
    {
        SingletonManager.Get<UIEvents>().OnActivateButton -= EnableButton;
    }

    public void OnButtonPressed()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Tutorial Panel");
    }

    private void EnableButton(string value, bool condition)
    {
        if (id != value) return;

        button.gameObject.SetActive(condition);
    }
}
