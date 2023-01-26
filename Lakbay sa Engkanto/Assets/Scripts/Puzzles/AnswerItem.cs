using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AnswerItem : MonoBehaviour
{
    [SerializeField] Animator Animator;

    public bool isTrue { get; set; }
    public string id;

    private Image sprite;
    private BoxCollider2D boxCollider2D;

    private void Awake()
    {
        sprite = GetComponent<Image>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>())
        {
            if (isTrue)
            {
                SingletonManager.Get<GameEvents>().PlayerCollectItem(id);
                Animator.SetTrigger("isCorrect");
            }
            else
            {
                SingletonManager.Get<GameEvents>().PlayerDamaged(1);
                Animator.SetTrigger("isWrong");
            }

            sprite.color = new Color(1, 1, 1, 0);
            boxCollider2D.enabled = false;
        }
    }
}
