using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TutorialScreen : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI descriptionBox;

    private TutorialData[] pages;
    private int currentPage;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    public void Initialize(TutorialData[] _tutorialDatas)
    {
        ClearData();

        pages = _tutorialDatas;

        currentPage = 0;
        ActivatePage(currentPage);
    }

    private void OnDisable()
    {
        ClearData();
    }

    public void OnExitButtonClicked()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Game Panel");
        Time.timeScale = 1f;
        ClearData();
    }

    public void OnNextButtonClicked()
    {
        currentPage++;

        Debug.Log("PRESSING");

        if (currentPage >= pages.Length)
        {
            currentPage = 0;
        }

        ActivatePage(currentPage);
    }

    public void OnPreviousButtonClicked()
    {
        currentPage--;

        Debug.Log("PRESSING");

        if (currentPage < 0)
        {
            currentPage = pages.Length - 1;
        }

        ActivatePage(currentPage);
    }

    public void ActivatePage(int pageNumber)
    {
        image.sprite = pages[pageNumber].ReferenceImage;
        descriptionBox.text = pages[pageNumber].Description;
    }

    private void ClearData()
    {
        pages = new TutorialData[0];
    }
}
