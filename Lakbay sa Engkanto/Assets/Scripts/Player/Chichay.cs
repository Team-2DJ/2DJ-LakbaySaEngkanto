using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chichay : MonoBehaviour
{
    public Transform Target { get; private set; }               // Player Follow Target
    public Animator Animator;                                   // Animator Controller Component Reference

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// Initializes Chichay Values
    /// </summary>
    /// <param name="value"></param>
    public void Initialize(Transform value)
    {
        Target = value;
    }
}
