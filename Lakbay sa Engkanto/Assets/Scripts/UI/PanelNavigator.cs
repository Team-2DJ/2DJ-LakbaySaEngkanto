using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelNavigator : MonoBehaviour
{
    public Transform PageHolder;
    public PageData[] Pages;

    private int currentPage;

    public struct PageData
    {
        public int PageNumber;
        public GameObject PageObject;
    }

    void OnEnable()
    {
        // Reset to First Page
        currentPage = 0;
        ActivatePage(currentPage);
    }

    void Awake()
    {
        // Initialize Page Holder
        Pages = new PageData[PageHolder.childCount];

        for (int i = 0; i < PageHolder.childCount; i++)
        {
            Pages[i].PageNumber = i;
            Pages[i].PageObject = PageHolder.GetChild(i).gameObject;
        }
    }

    // Return Button
    public void OnReturnButtonClicked()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("game-panel");
        Time.timeScale = 1f;
    }

    // Previous Button
    public void OnPreviousButtonClicked()
    {
        currentPage--;

        if (currentPage < 0)
        {
            currentPage = 0;
        }

        ActivatePage(currentPage);
    }

    // Next Button
    public void OnNextButtonClicked()
    {
        currentPage++;

        if (currentPage >= Pages.Length)
        {
            currentPage = Pages.Length - 1;
        }

        ActivatePage(currentPage);
    }

    // Activate Page Index
    public void ActivatePage(int pageNumber)
    {
        for (int i = 0; i < Pages.Length; i++)
        {
            Pages[i].PageObject.SetActive(Pages[i].PageNumber == pageNumber);
        }
    }
}
