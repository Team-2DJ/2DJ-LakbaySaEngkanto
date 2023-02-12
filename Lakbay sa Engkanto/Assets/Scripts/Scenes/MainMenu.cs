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

        // Reset Player Spawn Point
        SingletonManager.Get<PlayerManager>().PlayerSpawnPoint = new Vector2(0f, 0f);
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
