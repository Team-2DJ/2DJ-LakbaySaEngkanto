using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MangoTree : MonoBehaviour
{
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        SingletonManager.Get<GameEvents>().OnPlayerCollectItem += GrowMangoTree;
        animator = GetComponent<Animator>();
    }

    void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnPlayerCollectItem -= GrowMangoTree;
    }

    void GrowMangoTree()
    {
        animator.SetTrigger("isGrowing");
        Debug.Log("Tree is Growing");
    }
}
