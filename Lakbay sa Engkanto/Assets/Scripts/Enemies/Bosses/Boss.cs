using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : Enemy
{
    // Functions to be implemented in the future

    /// <summary>
    ///  Tells of the Attack Pattern of each derived boss type
    /// </summary>
    protected abstract void AttackPattern();
}
