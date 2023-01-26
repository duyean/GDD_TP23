using UnityEngine;
using UnityEngine.SceneManagement;
public class WinUI : Singleton<WinUI> {
    int finalLevelIndex = 4;

    [SerializeField]
    GameObject nextLevelButton;
    public GameObject winUI;
    public GameObject winMinigameUI;
    public GameObject loseMinigameUI;

    // checks whether this is last level and disables the next level button if true;
    private void Update() {
        if (SceneManager.GetActiveScene().name.Substring(6) == finalLevelIndex.ToString()) {
            nextLevelButton.SetActive(false);
        } else {
            nextLevelButton.SetActive(true);
        }
    }

    // function for loading next level using button
    public void nextLevel() {
        string sceneName = SceneManager.GetActiveScene().name;
        string subject = sceneName.Substring(0, 1);
        char level = sceneName[sceneName.Length - 1];
        int nextLevelNo = int.Parse(level.ToString()) + 1;

        SceneManager.LoadScene(subject + "Level" + nextLevelNo.ToString());


        winUI.SetActive(false);
        winMinigameUI.SetActive(false);
        loseMinigameUI.SetActive(false);
    }


    // function for loading minigame of the level using button
    public void loadMinigame() {
        string sceneName = SceneManager.GetActiveScene().name;
        string subject = sceneName.Substring(0, 1);
        char level = sceneName[sceneName.Length - 1];
        int levelNo = int.Parse(level.ToString());

        SceneManager.LoadScene(subject + "Minigame" + levelNo);


        winUI.SetActive(false);
        winMinigameUI.SetActive(false);
        loseMinigameUI.SetActive(false);
    }


    // go back to title
    public void backToTitle() {
        SceneManager.LoadScene("MainMenu");
        winUI.SetActive(false);
        winMinigameUI.SetActive(false);
        loseMinigameUI.SetActive(false);
    }

    // restarts the minigame level
    public void tryAgain() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        loseMinigameUI.SetActive(false);
    }
}
