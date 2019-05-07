﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private void Awake()
    {
        SetupSingleton();
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
}
