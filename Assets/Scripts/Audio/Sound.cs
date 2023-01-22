using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip AudioClip;

    // 0: background noise
    // 1: game music
    // 3: sfx
    public int soundType;
}
