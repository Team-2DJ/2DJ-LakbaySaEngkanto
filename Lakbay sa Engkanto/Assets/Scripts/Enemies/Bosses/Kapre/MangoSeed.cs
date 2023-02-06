using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]   // Requires the Box Collider to Function Correctly; 
public class MangoSeed : MonoBehaviour
{
    [SerializeField] private string id;

    private Collider2D playerCollider;

    private void Start()
    {
        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            //SingletonManager.Get<GameEvents>().PlayerCollectItem(id);

            
        }

        Destroy(this.gameObject);
    }
}
