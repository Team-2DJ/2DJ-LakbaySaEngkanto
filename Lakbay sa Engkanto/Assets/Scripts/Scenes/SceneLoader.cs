using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;
using System;

public class SceneLoader : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private string[] firstSceneIds;
    [SerializeField] private LoadingScreen loadingScreen;

    private string[] currentSceneIds;

    #region Singleton
    private void Awake()
    {
        SingletonManager.Register(this);
    }
    #endregion

    private void Start()
    {        
        LoadScene(firstSceneIds);
    }

    private IEnumerator LoadSceneSequence(string[] sceneIds)
    {
        // Activate Loading Screen
        loadingScreen.StartLoading();
        
        // Unload Current Scene if There are Any
        if (currentSceneIds != null)
        {
            foreach (string id in currentSceneIds)
                yield return SceneManager.UnloadSceneAsync(id);
        }
            
        // Unload Unused Assets
        Resources.UnloadUnusedAssets();
        yield return null;

        // Garbage Collection
        GC.Collect();
        yield return null;

        // Load the Scenes
        foreach (string id in sceneIds)
            yield return SceneManager.LoadSceneAsync(id, LoadSceneMode.Additive);

        // Deactivate Loading Screen
        loadingScreen.EndLoading();

        // Set Loaded Scene to Current Scene
        currentSceneIds = sceneIds;
    }

    public Coroutine LoadScene(string[] sceneIds)
    {
        return StartCoroutine(LoadSceneSequence(sceneIds));
    }
}
