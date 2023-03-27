using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CameraCutscene : Cutscenes
{
    [Header("References")]
    [SerializeField] private CinemachineVirtualCamera defaultCamera;                        // Current Room Camera Reference
    [SerializeField] private CinemachineVirtualCamera[] virtualCameras;                     // Array of Virtual Cameras

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    protected override void ExecuteCutscene()
    {
        base.ExecuteCutscene();

        StartCoroutine(CameraSequence());
        SingletonManager.Get<PanelManager>().ActivatePanel("");
    }

    /// <summary>
    /// Executes a Sequence of Cameras Turning On and Off One by One
    /// </summary>
    /// <returns></returns>
    IEnumerator CameraSequence()
    {
        // Deactivate Default Camera
        defaultCamera.gameObject.SetActive(false);
        
        // Pan Camera to Every Virtual Camera in the Array
        for (int i = 0; i < virtualCameras.Length; i++)
        {
            virtualCameras[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            virtualCameras[i].gameObject.SetActive(false);
        }

        // Reactivate Default Camera
        defaultCamera.gameObject.SetActive(true);

        // Go back to the Game Panel
        SingletonManager.Get<PanelManager>().ActivatePanel("Game Panel");

        // Enable Player Movement
        SingletonManager.Get<PlayerEvents>().SetPlayerMovement(true);
    }
}
