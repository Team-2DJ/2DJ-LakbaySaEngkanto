using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobileController : MonoBehaviour
{
    private void OnEnable()
    {
        //SingletonManager.Get<PlayerManager>().Player.PlayerMovement.HorizontalInput = 0f;
    }

    public void Move(float value)
    {
        SingletonManager.Get<PlayerManager>().Player.PlayerMovement.HorizontalInput = value;
    }

    public void Jump(bool value)
    {
        SingletonManager.Get<PlayerManager>().Player.PlayerMovement.Jump(value);
    }
}
