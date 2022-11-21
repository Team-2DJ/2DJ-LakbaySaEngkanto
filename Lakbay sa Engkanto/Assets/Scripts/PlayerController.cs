using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public void Move(float value)
    {
        SingletonManager.Get<PlayerManager>().Player.GetComponent<PlayerMovement>().HorizontalInput = value;
    }

    public void Jump(bool value)
    {
        SingletonManager.Get<PlayerManager>().Player.GetComponent<PlayerMovement>().Jump(value);
    }
}
