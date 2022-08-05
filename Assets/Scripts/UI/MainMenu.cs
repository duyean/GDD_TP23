using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject LevelSelection;

    public void SelectLevel()
    {
        // activating level selection UI
        LevelSelection.SetActive(true);
    }

    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenCustomize()
    {
        CharacterManager.Instance.OpenCustomizeMenu();
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void CloseLevelSelection()
    {
        // closes level selection used for back button in level select
        LevelSelection.SetActive(false) ;
    }
}
