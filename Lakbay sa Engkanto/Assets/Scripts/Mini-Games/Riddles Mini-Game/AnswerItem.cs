using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AnswerItem : MonoBehaviour
{
    public bool isTrue { get; set; }
    public string id;

    [SerializeField] private RiddlesMiniGame riddlesMiniGame;
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
                riddlesMiniGame.Animator.SetTrigger("isCorrect");
                riddlesMiniGame.CorrectAnswer();
            }
            else
            {
                SingletonManager.Get<GameEvents>().PlayerDamaged(1);
                riddlesMiniGame.Animator.SetTrigger("isWrong");
            }
        }
    }
}
