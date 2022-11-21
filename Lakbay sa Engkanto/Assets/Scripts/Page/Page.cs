using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{
    [SerializeField] string id;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>())
        {
            SingletonManager.Get<GameEvents>().PlayerCollectItem(id);
            Destroy(this.gameObject);
        }
    }
}
