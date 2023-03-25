using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructor : MonoBehaviour
{
    [SerializeField] private float destructionTime;
    
    void OnEnable()
    {
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(destructionTime);

        SingletonManager.Get<ObjectPooler>().ReturnToPool(gameObject);
    }
}
