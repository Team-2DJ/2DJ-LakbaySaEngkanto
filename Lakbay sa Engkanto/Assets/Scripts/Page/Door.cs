using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private string id;

    private void OnEnable()
    {
        SingletonManager.Get<GameEvents>().OnPlayerCollectItem += OpenDoor;
    }

    private void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnPlayerCollectItem -= OpenDoor;
    }

    private void OpenDoor(string id)
    {
        if (id == this.id)
            Destroy(this.gameObject);
    }
}
