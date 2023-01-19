using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class FormObj : MonoBehaviour {
    public TextMeshProUGUI levelQns, Subject;
    public TMP_InputField questions, imgUrl;
    public GameObject qnsType, insertImgObj, previewObj;
    public Image imgPreview;
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
                questions.fontAsset = GlobalVariables.instance.englishFont;
                break;
            case QuestionsObject.QuestionSubject.English:
                currentQns = connectFirebase.instance.englishQuestions[qId];
                questions.fontAsset = GlobalVariables.instance.englishFont;
                break;
            case QuestionsObject.QuestionSubject.Chinese:
                currentQns = connectFirebase.instance.chineseQuestions[qId];
                questions.fontAsset = GlobalVariables.instance.chineseFont;
                break;
        }
        if (currentQns == null) {
            currentQns = new QuestionsObject(sub, new List<string>(), new List<string>(), new List<string>(), "", "", 0, "");
        }
        for (int i = 0; i < answersBoxes.Length; i++) {
            answersBoxes[i].GetComponent<TMP_InputField>().fontAsset = (sub == QuestionsObject.QuestionSubject.Chinese ? GlobalVariables.instance.chineseFont : GlobalVariables.instance.englishFont);
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
        qnsType = currentQn;
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
        currentQns.qImage = imgUrl.text;
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
        previewObj.SetActive(true);
        previewObj.GetComponent<LoadQuestion>().tries = 3;
        previewObj.GetComponent<LoadQuestion>().qObj = currentQns;
        previewObj.GetComponent<LoadQuestion>().loadQuestion();
    }

    public void insImg(bool show) {
        insertImgObj.SetActive(show);
        if (show) {
            imgUrl.text = currentQns.qImage;
            if (imgUrl.text.Contains("http")) {
                StartCoroutine(LoadImageCoroutine(imgUrl.text, imgPreview));
            }
        }
    }

    public void updateImg() {
        if (imgUrl.text.Contains("http")) {
            StartCoroutine(LoadImageCoroutine(imgUrl.text, imgPreview));
        }
    }

    public IEnumerator LoadImageCoroutine(string url, Image gameObject) {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        var asyncOp = www.SendWebRequest();

        while (!www.isDone) {
            yield return null;
        }

        if (!string.IsNullOrEmpty(www.error)) {
            Debug.Log(www.error);
            Debug.Log(url);
        }
        var texture = DownloadHandlerTexture.GetContent(www);

        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));

        gameObject.sprite = sprite;
    }
}
