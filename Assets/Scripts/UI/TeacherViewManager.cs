using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeacherViewManager : MonoBehaviour {
    public GameObject questionScrollView, mainTeacherUI, questionSelect;
    public GameObject[] teacherViewMenu;
    private connectFirebase firebaseObj;
    public Sprite[] subjectIcon;
    public QuestionsObject.QuestionSubject currentSubject;
    public int questionId;
    private void Start() {
        firebaseObj = connectFirebase.instance;
    }
    public void switchUI(int id) {
        if (!mainTeacherUI.activeSelf) {
            mainTeacherUI.SetActive(true);
        }
        foreach (GameObject i in teacherViewMenu) {
            i.SetActive(false);
        }
        teacherViewMenu[id].SetActive(true);

        if (id == 3) {
            teacherViewMenu[3].GetComponent<FormObj>().updateData(currentSubject, questionId);
        }
    }

    public void selectSub(int sub) {
        currentSubject = (QuestionsObject.QuestionSubject)sub;
    }
    public void selectQuestionId(int qId) {
        questionId = qId;
        switchUI(3);
    }

    public void selectLevel(int level) {
        foreach (Transform child in questionScrollView.transform) {
            GameObject.Destroy(child.gameObject);
        }
        level += 1;
        float maxQid = 0.5f * level * (level + 1);
        int qId = (Mathf.RoundToInt(maxQid) - level) + 1;
        for (int i = 0; i < level; i++) {
            int _qid = qId;
            GameObject questionIdSelect = Instantiate(questionSelect, questionScrollView.transform);
            questionIdSelect.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Question " + (i + 1);
            questionIdSelect.GetComponent<Button>().onClick.AddListener(delegate { selectQuestionId(_qid); });
            qId += 1;
        }
    }
}
