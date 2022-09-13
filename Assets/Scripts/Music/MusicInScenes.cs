using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicInScenes : MonoBehaviour
{
    private void Start()
    {

        // checking if the scene starts with each letter that plays the individual correct level.
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName[0].ToString() == "c")
        {
            BGM.Instance.ChineseLevelBGM();
        }
        else if (currentSceneName[0].ToString() == "m")
        {
            BGM.Instance.MathLevelBGM();
        }
        else if (currentSceneName[0].ToString() == "e")
        {
            BGM.Instance.EnglishLevelBGM();
        }
        else
        {
            BGM.Instance.loadBGM();
        }

    }

    public void JumpSound()
    {
        SFX.Instance.JumpSound();
    }

    public void ClickSound()
    {
        SFX.Instance.ClickSound();
    }

    public void WinSound()
    {
        SFX.Instance.WinSound();
    }
    public void LoseSound()
    {
        SFX.Instance.LoseSound();
    }


}
