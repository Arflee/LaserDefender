using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer = null;
    [SerializeField] private string parameterToSet = null;

    private Slider volumeSlider;
    private string typeofSlider;

    private void Start()
    {
        volumeSlider = GetComponent<Slider>();

        typeofSlider = volumeSlider.CompareTag("Master") ? "Master" :
            CompareTag("Music") ? "Music" : 
                "Effects";

        if (PlayerPrefs.GetFloat(typeofSlider) != 0)
        {
            volumeSlider.value = PlayerPrefs.GetFloat(typeofSlider);
        }
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat(parameterToSet, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(typeofSlider, value);
    }
}
