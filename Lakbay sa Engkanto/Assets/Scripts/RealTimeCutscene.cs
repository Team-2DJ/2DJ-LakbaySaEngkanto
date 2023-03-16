using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RealTimeCutscene : Cutscenes
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    protected override void ExecuteCutscene()
    {
        base.ExecuteCutscene();

        Debug.Log("Real Time Cutscene");
    }
}
