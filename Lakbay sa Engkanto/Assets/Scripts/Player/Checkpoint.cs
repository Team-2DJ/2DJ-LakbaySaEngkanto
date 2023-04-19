using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Collider2D playerCollider;

    private bool isActivated;
    private Animator animator;

    private void OnEnable()
    {
        isActivated = SingletonManager.Get<PlayerManager>().PlayerSpawnPoint != (Vector2)transform.position ? false : true;
        animator.SetBool("isActivated", isActivated);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();  
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            if (isActivated)
                return;

            // Play SFX
            SingletonManager.Get<AudioManager>().Play("Computer");

            // Set This point as the Checkpoint
            SingletonManager.Get<PlayerManager>().PlayerSpawnPoint = (Vector2)transform.position;

            isActivated = true;
            animator.SetBool("isActivated", isActivated);
        }
    }
}
