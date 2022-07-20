using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFX : Singleton<SFX>
{
    [SerializeField] Slider SFXSlider;

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
        AudioListener.volume = SFXSlider.value;
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
}

