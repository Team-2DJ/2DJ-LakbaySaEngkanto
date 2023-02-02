using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private List<GameObject> PagesCollected = new List<GameObject>();

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        SingletonManager.Get<AdditionalSceneLoader>().LoadLevel();
    }

    public void RestartGame()
    {
        SingletonManager.Get<SceneLoader>().LoadScene("GameScene");
    }
}
