using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerItem : MonoBehaviour
{
    public bool isTrue { get; set; }
    public string id;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>())
        {
            if (isTrue)
                SingletonManager.Get<GameEvents>().PlayerCollectItem(id);
            else
            {
                SingletonManager.Get<GameEvents>().PlayerDamaged(1);
            }

            Destroy(this.gameObject);
        }
    }
}
