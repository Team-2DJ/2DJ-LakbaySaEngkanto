using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hazard : MonoBehaviour
{
    /// <summary>
    /// Used to implement Hazard behaviour per derived class
    /// </summary>
    public abstract void OnActHazard();

}
