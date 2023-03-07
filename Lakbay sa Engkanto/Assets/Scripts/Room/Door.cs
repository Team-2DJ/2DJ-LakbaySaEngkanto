using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private string id;
    [SerializeField] private int itemsLeft;

    private void OnEnable()
    {
        SingletonManager.Get<GameEvents>().OnOpenDoor += OpenDoor;
    }

    private void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnOpenDoor -= OpenDoor;
    }

    private void OpenDoor(string id)
    {
        if (id != this.id)
            return;

        if (--itemsLeft <= 0)
            Destroy(this.gameObject);
    }
}
