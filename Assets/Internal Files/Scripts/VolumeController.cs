using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer = null;
    [SerializeField] private string parameterToSet = null;

    public void SetVolume(float value)
    {
        audioMixer.SetFloat(parameterToSet, Mathf.Log10(value) * 20);
    }
}
