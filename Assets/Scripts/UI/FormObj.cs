using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FormObj : MonoBehaviour {
    public TextMeshProUGUI levelQns, Subject;
    public TMP_InputField questions;
    public GameObject qnsType;
    public GameObject[] answersBoxes;
    public GameObject[] typeToggle;
    public GameObject[] ansSelector;
    public QuestionsObject currentQns;

    public void Start() {
        //currentQns.answers
        //currentQns.finalAnswer
        //currentQns.question
        //currentQns.questionType = 0; 0 == Sort, 1 = Single, 2 = multi
    }

    public void updateData(QuestionsObject.QuestionSubject sub, int qId) {
        levelQns.text = qId.ToString();
        qId -= 1;
        Subject.text = sub.ToString();
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
        //not using TMP dropdown anymore. 
        changeQnsType(typeToggle[currentQns.questionType]);

        for (int i = 0; i < answersBoxes.Length; i++) {
            answersBoxes[i].GetComponent<TMP_InputField>().text = currentQns.answers[i];
        }
    }
    public void saveData() {
        currentQns.questionType = int.Parse(qnsType.name);
    }

    public void changeQnsType(GameObject currentQn) {
        qnsType = currentQn;
        foreach (GameObject qnType in typeToggle) {
            qnType.transform.GetChild(0).gameObject.SetActive(false);
        }
        currentQn.transform.GetChild(0).gameObject.SetActive(true);

        if (currentQn.name != "0") {
            foreach (GameObject selector in ansSelector) {
                selector.SetActive(true);
                selector.transform.GetChild(0).gameObject.SetActive(false);
            }
        } else {
            foreach (GameObject selector in ansSelector) {
                selector.SetActive(false);
            }
        }
    }

    public void selectAns(GameObject currentAns) {
        if (qnsType.name == "2") {
            foreach (GameObject selector in ansSelector) {
                selector.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        bool ans_state = currentAns.transform.GetChild(0).gameObject.activeSelf;
        currentAns.transform.GetChild(0).gameObject.SetActive(!ans_state);
    }

    public void save() {
        print("save");
    }

    public void preview() {
        print("prev");
    }

    public void insImg() {
        print("ins");
    }
}
