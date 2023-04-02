using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MangoTree : MonoBehaviour
{
    public Animator Animator { get; private set; }
    private Collider2D playerCollider;

    private void OnEnable()
    {
        SingletonManager.Get<GameEvents>().OnSeedCollected += StartGrowing;
    }

    private void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnSeedCollected -= StartGrowing;
    }

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            Debug.Log("End Game");
        }
    }

    void StartGrowing()
    {
        StartCoroutine(Grow());
    }

    IEnumerator Grow()
    {
        // Trigger Growing Animation
        Animator.SetTrigger("isGrowing");

        // Disable Player Movement
        SingletonManager.Get<PlayerEvents>().SetPlayerMovement(false);

        yield return new WaitForSeconds(3f);

        // Enable Player Movement
        SingletonManager.Get<PlayerEvents>().SetPlayerMovement(true);
    }
}