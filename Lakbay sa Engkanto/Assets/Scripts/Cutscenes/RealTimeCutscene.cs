using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(BoxCollider2D))]
public class RealTimeCutscene : Cutscenes
{
    [SerializeField] private PlayableDirector director;

    private void OnEnable()
    {
        director.stopped += EnablePlayer;
    }

    private void OnDisable()
    {
        director.stopped -= EnablePlayer;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    protected override void ExecuteCutscene()
    {
        base.ExecuteCutscene();

        Debug.Log("Real Time Cutscene");

        // Disable Player Movement
        SingletonManager.Get<PlayerEvents>().SetPlayerMovement(false);

        SingletonManager.Get<PanelManager>().ActivatePanel("");

        director.Play();
    }

    void EnablePlayer(PlayableDirector aDirector)
    {
        Debug.Log("ENABLE MOVEMENT ONCE AGAIN");
        SingletonManager.Get<PanelManager>().ActivatePanel("Game Panel");
        SingletonManager.Get<PlayerEvents>().SetPlayerMovement(true);
    }
}
