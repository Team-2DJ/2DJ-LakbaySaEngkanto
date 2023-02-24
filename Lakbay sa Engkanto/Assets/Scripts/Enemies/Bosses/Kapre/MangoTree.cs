using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MangoTree : MonoBehaviour
{
    private Animator animator;
    [SerializeField] string id;

    private void OnEnable()
    {
        SingletonManager.Get<GameEvents>().OnScoreChanged += GrowMangoTree;
    }

    void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnScoreChanged -= GrowMangoTree;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    // Set Mango Tree Animation based on Current Score and Winning Score Ratio
    void GrowMangoTree(string id, int currentScore, int winningScore)
    {
        if (id != this.id)
            return;

        animator.SetFloat("growth", (float)currentScore / (float)winningScore);
    }
}
