using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicInScenes : MonoBehaviour
{
    private void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName[0].ToString() == "c")
        {
            BGM.Instance.ChineseLevelBGM();
        }
        else if (currentSceneName[0].ToString() == "m")
        {
            //play math clip
        }
        else if (currentSceneName[0].ToString() == "e")
        {
            //play english clip
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
