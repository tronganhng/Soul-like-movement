using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    [Range(0, 1)] public float volume = 0.5f;
    [Range(.1f, 3)] public float pitch = 1f;
}
