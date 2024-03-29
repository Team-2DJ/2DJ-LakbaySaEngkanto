using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SingletonManager.Get<AudioManager>().Play("Main Menu BGM");

        // Activate Main Menu

        if (SingletonManager.Get<PlayerManager>().PlayerData.GameIsFinished)
        {
            SingletonManager.Get<PanelManager>().ActivatePanel("Credits");
        }
        else
        {
            SingletonManager.Get<PanelManager>().ActivatePanel("Main Menu");
        }
        

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

        if (SingletonManager.Get<PlayerManager>().PlayerData.GameIsFinished)
            SingletonManager.Get<PlayerManager>().PlayerData.GameIsFinished = false;
    }
    #endregion
    #endregion
}
