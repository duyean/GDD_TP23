using UnityEngine;
using UnityEngine.UI;
public class BGM : Singleton<BGM>
{
    [SerializeField] Slider BGMSlider;

    [SerializeField]
    AudioSource bgmSource;

    [SerializeField]
    AudioClip Theme;

    [SerializeField]
    AudioClip Mountain;

    [SerializeField]
    AudioClip Desert;

    [SerializeField]
    AudioClip City;


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
        bgmSource.volume = BGMSlider.value;
        saveBGMPref();
    }


    private void loadBGM()
    {
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        bgmSource.clip = Theme;
        bgmSource.Play();
    }

    private void saveBGMPref()
    {
        PlayerPrefs.SetFloat("BGMVolume", BGMSlider.value);
    }

    public void ChineseLevelBGM()
    {
        bgmSource.clip = Mountain;
        bgmSource.Play();
    }
}
