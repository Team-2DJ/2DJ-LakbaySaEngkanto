using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditionalSceneLoader : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadSceneAsync("GameUIScene", LoadSceneMode.Additive);
    }

    private void OnDisable()
    {
        SceneManager.UnloadSceneAsync("GameUIScene");

        // Get Level Index from Player Data
        string levelToLoad = "Level" + SingletonManager.Get<PlayerManager>().PlayerData.LevelIndex.ToString();

        // Load The Level Scene
        SceneManager.UnloadSceneAsync(levelToLoad);
    }

    #region Singleton
    private void Awake()
    {
        SingletonManager.Register(this);
    }
    #endregion

    // Loading Level Functionality
    public void LoadLevel()
    {
        // Get Level Index from Player Data
        string levelToLoad = "Level" + SingletonManager.Get<PlayerManager>().PlayerData.LevelIndex.ToString();

        // Load The Level Scene
        SceneManager.LoadSceneAsync(levelToLoad, LoadSceneMode.Additive);
    }
}
