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
    public string FirstSceneId;

    private string currentSceneId;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        LoadScene(FirstSceneId);
    }

    private IEnumerator LoadSceneSequence(string sceneId)
    {        
        if (!string.IsNullOrEmpty(currentSceneId))
        {
            AdditionalSceneLoader additionalScene = SingletonManager.Get<AdditionalSceneLoader>();

            if (additionalScene)
            {
                yield return additionalScene.UnloadScenes();
            }

            yield return SceneManager.UnloadSceneAsync(currentSceneId);
            currentSceneId = string.Empty;
        }

        Resources.UnloadUnusedAssets();
        yield return null; 
        GC.Collect();
        yield return null;

        yield return SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Additive);
        currentSceneId = sceneId;
    }

    public Coroutine LoadScene(string sceneId)
    {
        return StartCoroutine(LoadSceneSequence(sceneId));
    }
}
