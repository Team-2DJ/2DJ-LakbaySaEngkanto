using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Activate Main Menu
        SingletonManager.Get<PanelManager>().ActivatePanel("Main Menu");

        SingletonManager.Get<PlayerManager>().ResetProperties();
    }

    #region Button Functions
    #region Main Menu
    public void OnPlayButtonClicked()
    {
        string[] scenes = { "GameScene", "GameUIScene", "Level1" };

        SingletonManager.Get<SceneLoader>().LoadScene(scenes);
    }

    public void OnCreditsButtonClicked()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Credits");
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
        Debug.Log("You have quit the game!");
    }
    #endregion

    #region Credits Menu
    public void OnReturnButtonClicked()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Main Menu");
    }
    #endregion
    #endregion
}
