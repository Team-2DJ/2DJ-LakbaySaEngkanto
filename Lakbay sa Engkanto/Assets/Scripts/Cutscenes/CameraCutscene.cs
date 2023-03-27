using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CameraCutscene : Cutscenes
{
    [Header("References")]
    [SerializeField] private CinemachineVirtualCamera defaultCamera;
    [SerializeField] private CinemachineVirtualCamera[] virtualCameras;

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

    IEnumerator CameraSequence()
    {
        defaultCamera.gameObject.SetActive(false);
        
        for (int i = 0; i < virtualCameras.Length; i++)
        {
            virtualCameras[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            virtualCameras[i].gameObject.SetActive(false);
        }

        defaultCamera.gameObject.SetActive(true);
        SingletonManager.Get<PanelManager>().ActivatePanel("Game Panel");
        SingletonManager.Get<PlayerEvents>().SetPlayerMovement(true);
    }
}
