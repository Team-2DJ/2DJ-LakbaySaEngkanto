using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Audio", menuName = "Audio")]
public class AudioData : ScriptableObject
{
    // Indicates Type of Audio
    public enum AudioType
    {
        BGM,
        SFX
    };

    public string Id;                                                       // Audio ID
    public AudioClip Clip;                                                  // Audio Clip
    public AudioType Type;                                                  // Type of Audio

    [Range(0, 256)] public int Priority;                                    // Audio Priority
    [Range(0f, 1f)] public float Volume;                                    // Audio Volume
    [Range(0f, 3f)] public float Pitch;                                     // Audio Pitch
    public bool PlayOnAwake;                                                // Checks if Audio Should Play On Start-up
    public bool Loop;                                                       // Check if Audio Should Play On Repeat
    [Range(0f, 3f)] public float SpatialBlend;                              // Audio Spatial Blend

    public AudioSource Source { get; private set; }                         // Audio Source Component

    /// <summary>
    /// Initialize Audio Data Properties
    /// </summary>
    /// <param name="audioSourceContainer"></param>
    public void Initialize(GameObject audioSourceContainer)
    {
        Source = audioSourceContainer.AddComponent<AudioSource>();
        Source.clip = Clip;
        Source.priority = Priority;
        Source.volume = Volume;
        Source.pitch = Pitch;
        Source.playOnAwake = PlayOnAwake;
        Source.loop = Loop;
        Source.spatialBlend = SpatialBlend;
    }
}
