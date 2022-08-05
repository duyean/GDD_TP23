using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFX : Singleton<SFX>
{
    [SerializeField] Slider SFXSlider;

    [SerializeField]
    AudioSource sfxSource;

    [SerializeField]
    AudioClip click;

    [SerializeField]
    AudioClip jump;

    [SerializeField]
    AudioClip win;

    [SerializeField]
    AudioClip wrong;




    private void Start()
    {
        if (!PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.SetFloat("SFXVolume", 1);
            loadSFX();
        }
        else
        {
            loadSFX();
        }
    }

    public void changeSFXVolume()
    {
        sfxSource.volume = SFXSlider.value;
        saveSFXPref();
    }


    private void loadSFX()
    {
        SFXSlider.value = PlayerPrefs.GetFloat("BGMVolume");
    }

    private void saveSFXPref()
    {
        PlayerPrefs.SetFloat("SFXVolume", SFXSlider.value);
    }

    public void ClickSound()
    {
        sfxSource.PlayOneShot(click);
    }

    public void JumpSound()
    {
        sfxSource.PlayOneShot(jump);
    }

    public void WinSound()
    {

        sfxSource.PlayOneShot(win);
    }

    public void LoseSound()
    {
        sfxSource.PlayOneShot(wrong);
    }


}

