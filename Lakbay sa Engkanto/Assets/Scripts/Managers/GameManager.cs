using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        
    }

    public void RestartGame()
    {
        string[] scenes = { "GameScene", "Level" + SingletonManager.Get<PlayerManager>().PlayerData.LevelIndex.ToString(), "GameUIScene" };
        
        SingletonManager.Get<SceneLoader>().LoadScene(scenes);
    }
}
