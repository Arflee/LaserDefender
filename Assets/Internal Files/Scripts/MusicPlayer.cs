using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        SetupSingleton();
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ChangeVolume.musicChanged += VolumeChanged;
    }

    private void SetupSingleton()
    {
        int playersCount = FindObjectsOfType<MusicPlayer>().Length;
        if (playersCount > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void VolumeChanged(float volume)
    {
        audioSource.volume = volume;
    }
}
