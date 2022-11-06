using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cobweb : Hazard
{
    UnityEvent<float> SlowDownPlayer = new();
    UnityEvent<float> IncreasePlayerSpeed = new();
    [SerializeField] float speedModifier = 5f;
    private void Start()
    {
        SlowDownPlayer.AddListener(SingletonManager.Get<PlayerManager>().Player.GetComponent<PlayerMovement>().DividePlayerSpeed);
        IncreasePlayerSpeed.AddListener(SingletonManager.Get<PlayerManager>().Player.GetComponent<PlayerMovement>().MultiplyPlayerSpeed);
    }

    private void Update()
    {
        
    }
    public override void OnActHazard()
    {
        SlowDownPlayer.Invoke(speedModifier);
        Debug.Log("Hazard activated");
    }

    


    private void OnTriggerExit2D(Collider2D collision)
    {
        IncreasePlayerSpeed.Invoke(speedModifier);
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnActHazard();
    }
}
