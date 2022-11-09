using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyState
{
    ThrowStick, Idle
}

public class Kapre : Boss
{
    [Header("Setup")]
    [SerializeField] GameObject stick;
    [SerializeField] float fireRate;
    [SerializeField] float timeBetweenStates;
    private float nextFire;
    private Animator animator;

    EnemyState currentState;

    // Start is called before the first frame update
    void Start()
    {
        nextFire = Time.time;
        currentState = EnemyState.Idle;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                StartCoroutine(ChangeCurrentState());
                break;
            case EnemyState.ThrowStick:
                animator.SetTrigger("isThrowing");
                AttackPattern();
                StartCoroutine(ChangeCurrentState());
                break;
        }
    }

    protected override void AttackPattern()
    {
        if (Time.time > nextFire)
        {
            GameObject spawnedStick = Instantiate(stick, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>())
            DamagePlayer(1);
    }

    protected override void MovementPattern()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator ChangeCurrentState()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                yield return new WaitForSeconds(timeBetweenStates);
                currentState = EnemyState.ThrowStick; // Changes the state back to Throwing Stick
                break;
            case EnemyState.ThrowStick:
                yield return new WaitForSeconds(timeBetweenStates);
                currentState = EnemyState.Idle; // Changes the state back to Idle
                break;
        }
    }
}
