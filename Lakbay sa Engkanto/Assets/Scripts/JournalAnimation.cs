using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        animator.SetBool("isUpdated", SingletonManager.Get<PlayerManager>().PlayerData.JournalIsUpdated);
    }
}
