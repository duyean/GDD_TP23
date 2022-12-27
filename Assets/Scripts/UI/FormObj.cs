using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FormObj : MonoBehaviour {
    public TextMeshProUGUI levelQns, Subject;
    public TMP_InputField questions;
    public TMP_Dropdown qnsType;
    public TMP_InputField preFix, postFix;
    public GameObject[] ansbox;
    public Toggle[] toggleBoxes;
    public QuestionsObject currentQns;
    public void updateData(QuestionsObject.QuestionSubject sub, int qId) {
        qId -= 1;
        levelQns.text = "Level " + "0 " + "Question " + "0";
        Subject.text = "Subject: " + sub.ToString();
        switch (sub) {
            case QuestionsObject.QuestionSubject.Math:
                currentQns = connectFirebase.instance.mathQuestions[qId];
                break;
            case QuestionsObject.QuestionSubject.English:
                currentQns = connectFirebase.instance.englishQuestions[qId];
                break;
            case QuestionsObject.QuestionSubject.Chinese:
                currentQns = connectFirebase.instance.chineseQuestions[qId];
                break;
        }
        questions.text = currentQns.question;
        qnsType.value = currentQns.questionType;
        changeQnsType();
        preFix.text = currentQns.preFix;
        postFix.text = currentQns.postFix;
    }
    public void saveData() {

    }
    public void changeQnsType() {
        switch (qnsType.value) {
            case 0: //Arrange
                toggleBoxes[0].transform.parent.gameObject.SetActive(false);
                break;
            case 1: //MCQ
                toggleBoxes[0].transform.parent.gameObject.SetActive(true);
                GetComponent<ToggleGroup>().enabled = true;
                break;
            case 2: //MultiSelect
                toggleBoxes[0].transform.parent.gameObject.SetActive(true);
                GetComponent<ToggleGroup>().enabled = false;
                break;
        }
    }
}
