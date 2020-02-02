using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundClass
{
    public string clipName;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    //[Range(1f, 3f)]
    //public float pitch;
    [HideInInspector]
    public AudioSource source;

    public bool loop;
}
