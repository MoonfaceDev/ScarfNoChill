using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : BaseComponent
{
    public Sound[] sounds;
    public AudioSource sfxSource;

    public void PlaySFX(string name)
    {
        foreach (Sound sound in sounds)
            if (sound.Name == name)
            {
                print("playing sound - " + name);
                sfxSource.clip = sound.AudioClip;
                sfxSource.Stop();
                sfxSource.Play();
            }
    }

    public void StopSFX()
    {
         sfxSource.Stop();
    }
}
