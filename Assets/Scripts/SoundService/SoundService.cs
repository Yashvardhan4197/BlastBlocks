using System;
using UnityEngine;

public class SoundService
{
    private AudioSource bgAudioSource;
    private AudioSource sFXAudioSource;
    private SoundType[] soundTypes;

    public SoundService(AudioSource bgAudioSource, AudioSource sFXAudioSource, SoundType[] soundTypes)
    {
        this.bgAudioSource = bgAudioSource;
        this.sFXAudioSource = sFXAudioSource;
        this.soundTypes = soundTypes;
    }

    private AudioClip GetAudioClip(Sound soundName)
    {
        SoundType item = Array.Find(soundTypes, i => i.soundName == soundName);
        if (item == null)
        {
            return null;
        }
        return item.soundClip;
    }

    public void PlaySFX(Sound soundName)
    {
        AudioClip clip = GetAudioClip(soundName);
        if (clip != null)
        {
            if (sFXAudioSource.gameObject.activeSelf == true)
            {
                sFXAudioSource.PlayOneShot(clip);
            }
        }
    }

    public void PlayBackGroundAudio(Sound soundName)
    {
        AudioClip clip = GetAudioClip(soundName);
        if (clip != null)
        {
            bgAudioSource.clip = clip;
            bgAudioSource.Play();
        }
    }

    public void ToggleSFXAudioSource(bool toggle)
    {
        sFXAudioSource.gameObject.SetActive(toggle);
    }

    public void ToggleBGAudioSource(bool toggle)
    {
        bgAudioSource.gameObject.SetActive(toggle);
    }
}

[Serializable]
public class SoundType
{
    public Sound soundName;
    public AudioClip soundClip;
}

public enum Sound
{
    BACKGROUND_MUSIC,
    BUTTON_CLICK,
    PLAYER_CLICK,
    GAME_LOST,
    GAME_WON
}