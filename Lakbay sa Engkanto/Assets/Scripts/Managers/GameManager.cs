using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<GameObject> PagesCollected = new List<GameObject>();

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        SingletonManager.Get<PanelManager>().ActivatePanel("game-panel");
    }
}
