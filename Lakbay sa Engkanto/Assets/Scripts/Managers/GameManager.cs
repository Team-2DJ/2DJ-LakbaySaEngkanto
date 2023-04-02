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

    public void EndGame()
    {
        string[] scenes = { "EndingScene" };

        SingletonManager.Get<SceneLoader>().LoadScene(scenes);
    }

    public void RestartGame()
    {
        string[] scenes = { "GameScene", "GameUIScene", "Level1" };
        
        SingletonManager.Get<SceneLoader>().LoadScene(scenes);
    }
}
