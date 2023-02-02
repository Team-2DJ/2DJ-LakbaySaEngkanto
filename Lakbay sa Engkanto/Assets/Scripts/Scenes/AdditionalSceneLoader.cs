using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditionalSceneLoader : MonoBehaviour
{
    public string[] AdditionScenes;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        StartCoroutine(LoadSceneSequence());
    }

    private IEnumerator LoadSceneSequence()
    {
        for (int i = 0; i < AdditionScenes.Length; i++)
        {
            yield return SceneManager.LoadSceneAsync(AdditionScenes[i], LoadSceneMode.Additive);
        }
    }

    private IEnumerator UnloadSceneSequence()
    {
        for (int i = 0; i < AdditionScenes.Length; i++)
        {
            yield return SceneManager.UnloadSceneAsync(AdditionScenes[i]);
        }
    }

    public Coroutine UnloadScenes()
    {
        return StartCoroutine(UnloadSceneSequence());
    }
}
