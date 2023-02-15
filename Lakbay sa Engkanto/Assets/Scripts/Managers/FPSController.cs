using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField] int gameFrameRate;

    void Start()
    {
        Application.targetFrameRate = gameFrameRate;
    }

}
