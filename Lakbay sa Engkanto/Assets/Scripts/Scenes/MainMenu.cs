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

    public void Play()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("Level Selection");
    }

    public void OnLevelSelected(int level)
    {
        SingletonManager.Get<PlayerManager>().PlayerData.LevelIndex = level;

        string[] scenes = { "GameScene", "Level" + level, "GameUIScene" };

        SingletonManager.Get<SceneLoader>().LoadScene(scenes);
    }
}
