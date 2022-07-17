using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BGM : Singleton<BGM>
{
    [SerializeField] Slider BGMSlider;

    [SerializeField]
    AudioSource bgmSource;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("BGMVolume"))
        {
            PlayerPrefs.SetFloat("BGMVolume", 1);
            loadBGM();
        }
        else
        {
            loadBGM();
        }

    }
    public void changeBGMVolume()
    {
        AudioListener.volume = BGMSlider.value;
        saveBGMPref();
    }


    private void loadBGM()
    {
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume");
    }

    private void saveBGMPref()
    {
        PlayerPrefs.SetFloat("BGMVolume", BGMSlider.value);
    }
}
