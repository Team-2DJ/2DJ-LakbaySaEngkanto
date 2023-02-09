using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected string id;

    private Collider2D playerCollider;

    private void Start()
    {
        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();
    }

    protected abstract void ItemCollected();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider)
            ItemCollected();

        Destroy(this.gameObject);
    }

}
