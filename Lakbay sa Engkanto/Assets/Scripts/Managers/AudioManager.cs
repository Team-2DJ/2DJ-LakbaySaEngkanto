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

    #region Singleton
    void Awake()
    {
        SingletonManager.Register(this);

        // Initialize Audio Collections

        foreach (AudioData audioData in audioCollections)
        {
            switch (audioData.Type)
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
        // Find Audio in Audio Collections
        foreach (AudioData audioData in audioCollections)
        {
            // Audio Found
            if (id == audioData.Id)
            {
                audioData.Source.Play();
                return;
            }
        }

        // Audio Not Found
        Debug.LogWarning("Audio " + id + " cannot be found!");
        return;
    }

    /// <summary>
    /// Stop Audio
    /// </summary>
    /// <param name="id"></param>
    public void Stop(string id)
    {
        // Find Audio in Audio Collections
        foreach (AudioData audioData in audioCollections)
        {
            // Audio Found
            if (id == audioData.Id)
            {
                audioData.Source.Stop();
                return;
            }
        }

        // Audio Not Found
        Debug.LogWarning("Audio " + id + " cannot be found!");
        return;
    }

    /// <summary>
    /// Modify Audio Pitch
    /// </summary>
    /// <param name="id"></param>
    /// <param name="amount"></param>
    public void ModifyPitch(string id, float amount)
    {
        // Find Audio in Audio Collections
        foreach (AudioData audioData in audioCollections)
        {
            // Audio Found
            if (id == audioData.Id)
            {
                audioData.Source.pitch = amount;
                return;
            }
        }

        // Audio Not Found
        Debug.LogWarning("Audio " + id + " cannot be found!");
        return;
    }
    #endregion
}
