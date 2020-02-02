using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{

    public SoundClass[] sounds;
    public PlayerSounds[] characterSounds;

    static SoundManager()
    {
        Instance = Instance;
    }

    public static SoundManager Instance { get; private set; }

    //TODO: singleton

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (SoundClass sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }

        for (int i = 0; i < characterSounds.Length; i++)
        {
            foreach (SoundClass sound in characterSounds[i].personalEffect)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;
            }
        }
    }

    private void Start()
    {
        Play("Intro");
        Play("Main Menu");
    }

    public void Play(string clipName)
    {
        SoundClass s = Array.Find(sounds, sound => sound.clipName == clipName);
        if (s == null)
        {
            Debug.LogWarning("Clip: " + clipName + " not found. Check the name");
            return;
        }
        s.source.Play();
    }

    public void Stop(string clipName)
    {
        SoundClass s = Array.Find(sounds, sound => sound.clipName == clipName);
        if (s == null)
        {
            Debug.LogWarning("Clip: " + clipName + " not found. Check the name");
            return;
        }
        s.source.Stop();
    }

    public void PlayCharacterSound(string clipName)
    {
        string playerName = clipName.Split('_')[0];
        PlayerSounds player = Array.Find(characterSounds, pl => pl.name == playerName);

        if (player != null)
        {
            foreach (SoundClass sound in player.personalEffect)
            {
                if (sound.clipName == clipName)
                {
                    sound.source.Play();
                    return;
                }
            }
        }

        Debug.LogWarning("Clip: " + clipName + " not found. Check the name");
        return;
    }

    public void StopCharacterSound(string clipName)
    {

        string playerName = clipName.Split('_')[0];
        PlayerSounds player = Array.Find(characterSounds, pl => pl.name == playerName);

        if (player != null)
        {
            foreach (SoundClass sound in player.personalEffect)
            {
                if (sound.clipName == clipName)
                {
                    sound.source.Stop();
                    return;
                }
            }
        }

        Debug.LogWarning("Clip: " + clipName + " not found. Check the name");
        return;
    }
}