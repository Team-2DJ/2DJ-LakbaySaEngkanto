using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    #region Singleton
    private void Awake()
    {
        SingletonManager.Register(this);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        string currentMusic = SingletonManager.Get<PlayerManager>().PlayerData.GameMusicID;

        SingletonManager.Get<AudioManager>().Play(currentMusic);
    }

    /// <summary>
    /// Change Background Music
    /// </summary>
    /// <param name="newMusic"></param>
    public void ChangeMusic(string newMusic)
    {
        string tempMusic = SingletonManager.Get<PlayerManager>().PlayerData.GameMusicID;

        if (tempMusic == newMusic)
            return;

        // Stop Old Music
        SingletonManager.Get<AudioManager>().Stop(tempMusic);

        // Play New Music
        if (newMusic != null)
        {
            SingletonManager.Get<PlayerManager>().PlayerData.GameMusicID = newMusic;
            SingletonManager.Get<AudioManager>().Play(newMusic);
        }
            
    }
}
