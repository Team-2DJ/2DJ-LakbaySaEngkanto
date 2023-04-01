using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journal : MonoBehaviour
{
    [SerializeField] private Transform pageHolder;
    private PageData[] pages;

    private int currentPage;

    public int Level { get; private set; }

    public struct PageData
    {
        public int PageNumber;
        public GameObject PageObject;
    }

    void OnEnable()
    {
        int pagesCollected = SingletonManager.Get<PlayerManager>().PlayerData.PagesCollected;
        Level = pagesCollected;

        // Reset to First Page
        currentPage = 0;
        ActivatePage(currentPage);
    }

    private void Awake()
    {
        // Initialize Page Holder
        pages = new PageData[pageHolder.childCount];

        for (int i = 0; i < pageHolder.childCount; i++)
        {
            pages[i].PageNumber = i;
            pages[i].PageObject = pageHolder.GetChild(i).gameObject;
        }
    }

    void Start()
    {

    }

    /// <summary>
    /// Close Button
    /// </summary>
    public void OnCloseButtonClicked()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Game Panel");
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Previous Button
    /// </summary>
    public void OnPreviousButtonClicked()
    {
        currentPage--;

        if (currentPage < 0)
        {
            currentPage = 0;
        }

        ActivatePage(currentPage);
    }

    /// <summary>
    /// Next Button
    /// </summary>
    public void OnNextButtonClicked()
    {
        if (currentPage >= Level)
        {
            Debug.Log("Not yet unlocked");
            return;
        }


        currentPage++;

        if (currentPage >= pages.Length)
        {
            currentPage = pages.Length - 1;
        }

        ActivatePage(currentPage);
    }

    /// <summary>
    /// Activate Page Index
    /// </summary>
    /// <param name="pageNumber"></param>
    public void ActivatePage(int pageNumber)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].PageObject.SetActive(pages[i].PageNumber == pageNumber);
        }
    }
}
