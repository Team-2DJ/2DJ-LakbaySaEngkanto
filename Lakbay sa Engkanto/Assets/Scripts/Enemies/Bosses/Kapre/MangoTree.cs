using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MangoTree : MonoBehaviour
{
    private Animator animator;
    [SerializeField] string id;

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

    void GrowMangoTree(string id)
    {
        if (id == this.id)
        {
            animator.SetTrigger("isGrowing");
            Debug.Log("Tree is Growing");
        }
    }
}
