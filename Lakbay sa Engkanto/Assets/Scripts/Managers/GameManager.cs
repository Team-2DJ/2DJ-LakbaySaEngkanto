using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private void Awake()
    {
        SingletonManager.Register(this);
    }
    #endregion

    private void Start()
    {
        
    }

    public void RestartGame()
    {
        string[] scenes = { "GameScene", "GameUIScene", "Level" + SingletonManager.Get<PlayerManager>().PlayerData.LevelIndex.ToString() };
        
        SingletonManager.Get<SceneLoader>().LoadScene(scenes);
    }
}
