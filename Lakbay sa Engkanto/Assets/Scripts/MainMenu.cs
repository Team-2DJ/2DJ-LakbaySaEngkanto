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
    }

    public void Play()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Level Selection");
    }

    public void OnLevelSelected(int level)
    {
        SingletonManager.Get<PlayerManager>().PlayerData.LevelIndex = level;

        SingletonManager.Get<SceneLoader>().LoadScene("GameScene");
    }
}
