using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateManager : MonoBehaviour
{
    [SerializeField] int gameFrameRate;

    void Start()
    {
        Application.targetFrameRate = gameFrameRate;
    }

}
