using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject LevelSelection;

    private void Start()
    {
        // play sound
    }

    public void SelectLevel()
    {
        // activating level selection UI
        LevelSelection.SetActive(true);
    }

}
