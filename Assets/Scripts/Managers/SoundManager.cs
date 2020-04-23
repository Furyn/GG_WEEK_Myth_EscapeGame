using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioSource musicSource;
    public AudioSource[] SFXSource;

    public Slider musicSlider;
    public Slider SFXSlider;

    public bool isMusicOn = true;
    public bool isSFXOn = true;

    public override void Init()
    {
        base.Init();
    }

    public void ChangeVolumeSFX()
    {
        for(int i = 0; i < this.SFXSource.Length; i++)
        {
            this.SFXSource[i].volume = SFXSlider.value;
            Debug.Log("SFX volume set to " + SFXSource[i].volume);
        }
    }

    public void ChangeVolumeMusic()
    {
            musicSource.volume = musicSlider.value;
            Debug.Log("Music volume set to " + musicSource.volume);
    }

    public void ToggleMusic()
    {
        if(isMusicOn)
        {
            musicSource.volume = 0f;
        }
        else
        {
            ChangeVolumeMusic();
        }

        isMusicOn = !isMusicOn;
    }

    public void ToggleSFX()
    {
        if (isSFXOn)
        {
            for (int i = 0; i < this.SFXSource.Length; i++)
            {
                this.SFXSource[i].volume = 0;
            }
        }
        else
        {
            ChangeVolumeSFX();
        }

        isSFXOn = !isSFXOn;
    }

    public void PlaySFX(AudioSource SFXToPlay)
    {
        SFXToPlay.Play();
    }

    public void PlayMusic(AudioSource MusicToPlay)
    {
        MusicToPlay.Play();
    }
}
