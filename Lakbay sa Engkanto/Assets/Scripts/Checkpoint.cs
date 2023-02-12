using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
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
            // Set This point as the Checkpoint
            SingletonManager.Get<PlayerManager>().PlayerSpawnPoint = transform.position;
        }
    }
}
