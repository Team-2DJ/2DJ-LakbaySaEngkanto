using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructor : MonoBehaviour
{
    [SerializeField] private float destructionTime;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(destructionTime);

        SingletonManager.Get<ObjectPooler>().ReturnToPool(gameObject);
    }
}
