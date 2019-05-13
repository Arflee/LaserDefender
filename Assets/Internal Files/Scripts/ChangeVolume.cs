using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    //[SerializeField]

    public delegate void OnMusicVolumeChanged(float volume);
    public delegate void OnEffectsVolumeChanged(float volume);

    public static OnMusicVolumeChanged musicChanged;
    public static OnEffectsVolumeChanged effectsChanged;

    public void ChangeMusicVolume(float volume)
    {
        musicChanged?.Invoke(volume);
    }

    public void ChangeEffectsVolume(float volume)
    {
        effectsChanged?.Invoke(volume);
    }
}
