using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MusicTrigger : MonoBehaviour
{
    [SerializeField] private string id;                             // ID to Ensure this ony Gets Triggered Once Per Game
    [SerializeField] private string musicId;                        // ID of Music to Play
    
    private Collider2D playerCollider;
    private bool isActivated;


    // Start is called before the first frame update
    void Start()
    {
        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();

        if (SingletonManager.Get<PlayerManager>().PlayerData.StringList.Contains(id))
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isActivated)
            return;
        
        if (other == playerCollider)
        {
            SingletonManager.Get<MusicManager>().ChangeMusic(musicId);
            isActivated = true;

            SingletonManager.Get<PlayerManager>().PlayerData.AddString(id);
        }
    }



}
