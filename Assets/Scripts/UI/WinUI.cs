using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinUI : Singleton<WinUI>
{
    int finalLevelIndex = 4;

    [SerializeField]
    GameObject nextLevelButton;
    public GameObject winUI;

    private void Update()
    {
        if(SceneManager.GetActiveScene().name.Substring(6) == finalLevelIndex.ToString())
        {
            nextLevelButton.SetActive(false);
        }
        else
        {
            nextLevelButton.SetActive(true);
        }
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name.Substring(0,6) + (int.Parse(SceneManager.GetActiveScene().name.Substring(6)) + 1).ToString());
    }

    public void backToTitle()
    {
        SceneManager.LoadScene("MainMenu");
        WinUI.Instance.winUI.SetActive(false) ;
    }
}
