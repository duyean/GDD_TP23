using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public GameObject LevelSelection, subjectSelect, playBtnObj;
    public QuestionsObject.QuestionSubject subject;

    public void playBtn() {
        // activating level selection UI
        subjectSelect.SetActive(true);
        playBtnObj.SetActive(false);
    }

    public void StartGame(int level) {
        string loadLevel = subject.ToString().Substring(0, 1).ToLower() + "Level" + level;
        SceneManager.LoadScene(loadLevel);
    }

    public void OpenCustomize() {
        CharacterManager.Instance.OpenCustomizeMenu();
    }

    public void CloseLevelSelection() {
        // closes level selection used for back button in level select
        LevelSelection.SetActive(false);
    }
    public void selectSubject(int subjectID) {
        subject = (QuestionsObject.QuestionSubject)subjectID;
        LevelSelection.SetActive(true);
        subjectSelect.SetActive(false);

    }
}
