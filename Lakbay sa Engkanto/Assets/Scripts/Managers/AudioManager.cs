using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game Audio System, Manages Music and Sound Effects
/// </summary>
public class AudioManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioData[] audioCollections;                  // Collection of Audio Data

    [Space]
    [SerializeField] private GameObject musicHolder;                        // Holds All Musics/BGM's
    [SerializeField] private GameObject soundHolder;                        // Holds All SFX

    private Dictionary<string, AudioData> audioDictionary = new Dictionary<string, AudioData>();

    #region Singleton
    void Awake()
    {
        SingletonManager.Register(this);

        // Initialize Audio Collections

        foreach (AudioData audioData in audioCollections)
        {

            audioDictionary.Add(audioData.GetId(), audioData);

            switch (audioData.GetAudioType())
            {
                case AudioData.AudioType.BGM:
                    audioData.Initialize(musicHolder);
                    break;

                case AudioData.AudioType.SFX:
                    audioData.Initialize(soundHolder);
                    break;
            };
        }

    }
    #endregion

    #region Audio Methods
    /// <summary>
    /// Play Audio
    /// </summary>
    /// <param name="id"></param>
    public void Play(string id)
    {
        // Audio Not Found
        if (!audioDictionary.ContainsKey(id))
        {
            Debug.LogWarning("Audio " + id + " cannot be found!");
            return;
        }

        // Finds the key (ID) inside the audioDictionary
        // Afterwards, plays the audio. 
        audioDictionary[id].Source.Play();
    }

    /// <summary>
    /// Play an instance of the audio
    /// </summary>
    /// <param name="id"></param>
    public void PlayOneShot(string id)
    {
        // Audio Not Found
        if (!audioDictionary.ContainsKey(id))
        {
            Debug.LogWarning("Audio " + id + " cannot be found!");
            return;
        }

        // Finds the key (ID) inside the audioDictionary
        // Afterwards, play an audio instance. 
        audioDictionary[id].Source.PlayOneShot(audioDictionary[id].Source.clip);
    }

    /// <summary>
    /// Stop Audio
    /// </summary>
    /// <param name="id"></param>
    public void Stop(string id)
    {
        // Audio Not Found
        if (!audioDictionary.ContainsKey(id))
        {
            Debug.LogWarning("Audio " + id + " cannot be found!");
            return;
        }


        // Finds the key (ID) inside the audioDictionary
        // Afterwards, stops the audio. 
        audioDictionary[id].Source.Stop();
    }

    /// <summary>
    /// Modify Audio Pitch
    /// </summary>
    /// <param name="id"></param>
    /// <param name="amount"></param>
    public void ModifyPitch(string id, float amount)
    {
        // Audio Not Found
        if (!audioDictionary.ContainsKey(id))
        {
            Debug.LogWarning("Audio " + id + " cannot be found!");
            return;
        }

        // Finds the key (ID) inside the audioDictionary
        // Afterwards, adjusts the pitch based on the amount. 
        audioDictionary[id].Source.pitch = amount;

    }
    #endregion
}
