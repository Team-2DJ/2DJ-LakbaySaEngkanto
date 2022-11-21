using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MangoSeed : MonoBehaviour
{
    [SerializeField] string id;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>())
        {
            SingletonManager.Get<GameEvents>().PlayerCollectItem(id);
            Destroy(this.gameObject);
        }
    }
}
