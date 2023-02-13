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
        SingletonManager.Get<GameEvents>().OnScoreChanged += GrowMangoTree;
        animator = GetComponent<Animator>();
    }

    void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnScoreChanged -= GrowMangoTree;
    }


    // Set Mango Tree Animation based on Current Score and Winning Score Ratio
    void GrowMangoTree(string id, int currentScore, int winningScore)
    {
        if (id != this.id)
            return;

        animator.SetFloat("growth", (float)currentScore / (float)winningScore);
    }
}
