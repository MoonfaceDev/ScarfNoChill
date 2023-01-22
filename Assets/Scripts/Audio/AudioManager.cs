using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : BaseComponent
{
    public Sound[] sounds;
    public int[] playInBackround;

    public AudioSource backgroundNoiseSource, musicSource, sfxSource;

    private void Awake()
    {
        foreach (Sound sound in sounds)
        {
            if (sound.soundType == 0)
                PlayBackgroundNoise(sound);
            if (sound.soundType == 1)
                PlayMusic(sound);
        }
    }

    public void PlayBackgroundNoise(Sound sound)
    {
        backgroundNoiseSource.clip = sound.AudioClip;
        backgroundNoiseSource.loop = true;
        backgroundNoiseSource.Play();
    }

    public void PlayMusic(Sound sound)
    {
        musicSource.clip = sound.AudioClip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(string name)
    {
        foreach (Sound sound in sounds)
            if (sound.Name == name)
            {
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
