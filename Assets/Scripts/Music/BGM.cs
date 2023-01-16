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
    AudioClip Chinese_BGM;

    [SerializeField]
    AudioClip Math_BGM;

    [SerializeField]
    AudioClip English_BGM;


    private void Start()
    {
        // if volume is not set previously, meaning first session of play, then set bgm to max
        if (!PlayerPrefs.HasKey("BGMVolume"))
        {
            PlayerPrefs.SetFloat("BGMVolume", 0.3083128f);
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


    public void loadBGM()
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
        bgmSource.clip = Chinese_BGM;
        bgmSource.Play();
    }

    public void MathLevelBGM()
    {
        bgmSource.clip = Math_BGM;
        bgmSource.Play();
    }

    public void EnglishLevelBGM()
    {
        bgmSource.clip = English_BGM;
        bgmSource.Play();
    }

}
