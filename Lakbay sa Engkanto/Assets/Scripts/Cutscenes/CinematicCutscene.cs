using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CinematicCutscene : Cutscenes
{
    [SerializeField] private Sprite[] panelSprites;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    protected override void ExecuteCutscene()
    {
        base.ExecuteCutscene();

        SingletonManager.Get<PanelManager>().ActivatePanel("Interactive Cutscene Panel");
        SingletonManager.Get<CinematicCutsceneManager>().Initialize(panelSprites);
    }
}
