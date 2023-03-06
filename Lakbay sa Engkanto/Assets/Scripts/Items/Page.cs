using System.Collections;
using UnityEngine;

public class Page : Item
{
    Collider2D pageCollider;

    protected override void Start()
    {
        base.Start();

        pageCollider = GetComponent<Collider2D>();
        pageCollider.enabled = false;

        StartCoroutine(EnableCollider());
    }

    protected override void ItemCollected()
    {
        SingletonManager.Get<GameEvents>().PlayerCollectItem(id);
    }

    IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(1f);

        pageCollider.enabled = true;
    }
}