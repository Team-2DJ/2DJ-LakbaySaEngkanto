using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ChichayTrigger : MonoBehaviour
{
    private Collider2D playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            if (SingletonManager.Get<PlayerManager>().PlayerData.HasMetChichay)
                return;

            // Spawn Chichay
            SingletonManager.Get<PlayerManager>().Player.SpawnChichay();

            // Indicate Player Has Encountered Chichay
            SingletonManager.Get<PlayerManager>().PlayerData.HasMetChichay = true;
        }
    }
}
