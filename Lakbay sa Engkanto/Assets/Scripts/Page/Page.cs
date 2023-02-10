using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : Item
{
    Collider2D pageCollider;
    
    private void OnEnable()
    {
        pageCollider = GetComponent<Collider2D>();
        pageCollider.enabled = false;
        Debug.Log("false collider");

        StartCoroutine(EnableCollider());
    }

    protected override void ItemCollected()
    {
        SingletonManager.Get<GameEvents>().PlayerCollectItem(id);
    }

    IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(1f);

        Debug.Log("true collider");
        pageCollider.enabled = true;
    }
}
