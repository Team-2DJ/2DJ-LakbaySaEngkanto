using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SingletonManager.Get<GameEvents>().PickupPage();
        Destroy(this.gameObject);
    }
}
