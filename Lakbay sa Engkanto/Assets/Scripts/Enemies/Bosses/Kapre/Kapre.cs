using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kapre : Boss
{
    [Header("Setup")]
    [SerializeField] GameObject stick;
    [SerializeField] float fireRate;
    private float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        AttackPattern();
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
}
