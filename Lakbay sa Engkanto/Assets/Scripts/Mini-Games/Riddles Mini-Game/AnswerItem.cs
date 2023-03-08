using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AnswerItem : MonoBehaviour
{
    public bool isTrue { get; set; }
    [SerializeField] private string correctID;
    [SerializeField] private string wrongID;

    private Collider2D playerCollider;

    private void Start()
    {
        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            if (isTrue)
            {
                // If is true, invoke the event
                SingletonManager.Get<PlayerEvents>().PlayerCollectItem(correctID);
            }
            else
            {
                SingletonManager.Get<PlayerEvents>().PlayerCollectItem(wrongID);
                SingletonManager.Get<PlayerEvents>().PlayerDamaged(1);
            }
        }
    }
}
