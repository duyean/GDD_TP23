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
    private QuestionsObject.QuestionSubject subject;
    private int arrayQId;

    public void Start() {
        //currentQns.answers
        //currentQns.finalAnswer
        //currentQns.question
        //currentQns.questionType = 0; 0 == Sort, 1 = Single, 2 = multi
    }

    public void updateData(QuestionsObject.QuestionSubject sub, int qId) {
        levelQns.text = qId.ToString();
        qId -= 1;
        arrayQId = qId;
        Subject.text = sub.ToString();
        subject = sub;
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
        if (currentQns == null) {
            currentQns = new QuestionsObject(sub, new List<string>(), new List<string>(), new List<string>(), "", "", 0, "");
        }

        questions.text = currentQns.question;
        if (currentQns.answers.Count != 0) {
            for (int i = 0; i < currentQns.answers.Count; i++) {
                answersBoxes[i].GetComponent<TMP_InputField>().text = currentQns.answers[i];
            }
        }
        changeQnsType(typeToggle[currentQns.questionType]);
    }

    public void changeQnsType(GameObject currentQn) {
        foreach (GameObject qnType in typeToggle) {
            qnType.transform.GetChild(0).gameObject.SetActive(false);
        }
        currentQn.transform.GetChild(0).gameObject.SetActive(true);

        if (currentQn.name != "0") {
            for (int i = 0; i < ansSelector.Length; i++) {
                ansSelector[i].SetActive(true);
                ansSelector[i].transform.GetChild(0).gameObject.SetActive(false);
                if (answersBoxes[i].GetComponent<TMP_InputField>().text != "") {
                    if (currentQns.finalAnswer.Contains(answersBoxes[i].GetComponent<TMP_InputField>().text)) {
                        ansSelector[i].transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
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
        currentQns.question = questions.text;
        currentQns.questionType = int.Parse(qnsType.name);
        if (currentQns.finalAnswer.Count > 0) {
            currentQns.finalAnswer.Clear();
        }
        if (currentQns.answers.Count > 0) {
            currentQns.answers.Clear();
        }
        if (answersBoxes.Length != 0) {
            for (int i = 0; i < answersBoxes.Length; i++) {
                currentQns.answers.Add(answersBoxes[i].GetComponent<TMP_InputField>().text);
                if (ansSelector[i].transform.GetChild(0).gameObject.activeSelf) {
                    currentQns.finalAnswer.Add(answersBoxes[i].GetComponent<TMP_InputField>().text);
                }
            }
        }


        switch (subject) {
            case QuestionsObject.QuestionSubject.Math:
                connectFirebase.instance.mathQuestions[arrayQId] = currentQns;
                break;
            case QuestionsObject.QuestionSubject.English:
                connectFirebase.instance.englishQuestions[arrayQId] = currentQns;
                break;
            case QuestionsObject.QuestionSubject.Chinese:
                connectFirebase.instance.chineseQuestions[arrayQId] = currentQns;
                break;
        }
        connectFirebase.instance.uploadToDatabase();
    }

    public void preview() {
        print("prev");
    }

    public void insImg() {
        print("ins");
    }
}
