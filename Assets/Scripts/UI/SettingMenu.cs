using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingMenu : Singleton<SettingMenu>
{
    public GameObject settingMenu;

    private void Start()
    {
        SceneManager.LoadScene("cMinigame0");
    }
    public void openMenu()
    {
        settingMenu.SetActive(true);
    }

    public void back()
    {
        settingMenu.SetActive(false);
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
        settingMenu.SetActive(false);
    }

}
