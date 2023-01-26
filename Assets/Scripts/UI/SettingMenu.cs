using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingMenu : Singleton<SettingMenu> {
    public GameObject settingMenu;

    private void Start() {
        SceneManager.LoadScene("MainMenu");
    }
    public void openMenu() {
        settingMenu.SetActive(true);
    }

    public void back() {
        settingMenu.SetActive(false);
    }

    public void Menu() {
        GlobalVariables.instance.questionsInMap = 0;
        SceneManager.LoadScene("MainMenu");
        settingMenu.SetActive(false);
    }
    public void QuitGame() {
        Application.Quit();
    }

}
