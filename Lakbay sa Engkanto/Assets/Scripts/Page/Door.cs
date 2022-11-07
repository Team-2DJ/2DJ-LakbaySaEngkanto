using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    private void Start()
    {
        SingletonManager.Get<GameEvents>().OnPickupPage += OpenDoor;
    }
    

    private void OpenDoor()
    {
        Destroy(this.gameObject);
    }

    private void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnPickupPage -= OpenDoor;
    }
}
