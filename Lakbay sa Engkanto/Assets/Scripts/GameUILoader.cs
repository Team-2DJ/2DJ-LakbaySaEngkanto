using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Loads-Up the In-Game UI
public class GameUILoader : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadSceneAsync("GameUIScene", LoadSceneMode.Additive);
    }

    private void OnDisable()
    {
        SceneManager.UnloadSceneAsync("GameUIScene");
    }
}
