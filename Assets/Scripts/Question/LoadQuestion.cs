using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class LoadQuestion : MonoBehaviour {
    public GameObject answeringSlot, answers, answerView, possibleAnswerView, hint, incorrect;
    public TextMeshProUGUI qns, triesLeft;
    public QuestionsObject.QuestionSubject subject;
    public GameObject winObj, UIDisplay;
    public int level, tries;
    private List<string> rndList = new List<string>();
    private IDictionary<string, int> ansKey = new Dictionary<string, int>();

    private QuestionsObject qObj;

    public void loadQuestion() {
        if (ansKey.Count > 0) {
            ansKey.Clear();
        }
        int maxQid = Mathf.RoundToInt(0.5f * level * (level + 1));
        maxQid = (maxQid - GlobalVariables.instance.questionsInMap);
        switch (subject) {
            case QuestionsObject.QuestionSubject.Math:
                qObj = connectFirebase.instance.mathQuestions[maxQid];
                break;
            case QuestionsObject.QuestionSubject.English:
                qObj = connectFirebase.instance.englishQuestions[maxQid];
                break;
            case QuestionsObject.QuestionSubject.Chinese:
                qObj = connectFirebase.instance.chineseQuestions[maxQid];
                break;
        }
        if (qObj == null) {
            solved();
            return;
        }
        qns.text = qObj.question;
        int i = 0;
        rndList = qObj.answers;
        if (qObj.questionType == 0) {
            foreach (string s in qObj.answers) {
                if (s.Length > 0) {
                    ansKey.Add(new KeyValuePair<string, int>(s, i));
                    i++;
                }
            }
            rndList = Shuffle(rndList);
        } else {
            foreach (string s in qObj.finalAnswer) {
                if (s.Length > 0) {
                    i = qObj.answers.IndexOf(s);
                    ansKey.Add(new KeyValuePair<string, int>(s, i));
                }
            }
        }
        int loopIndex = 0;
        foreach (string s in rndList) {

            if (s.Length > 0) {
                GameObject newAns = Instantiate(answers);
                if (s.Contains("http")) {
                    StartCoroutine(LoadImageCoroutine(s, newAns));
                } else {
                    newAns.GetComponentInChildren<TextMeshProUGUI>().text = s;
                }
                newAns.transform.SetParent(answerView.transform);
                newAns.transform.localScale = new Vector3(2f, 2f, 2f);
                int value;
                newAns.GetComponent<DragDrop>().itemValue = loopIndex.ToString();
                loopIndex++;
                if (qObj.questionType == 0) {
                    if (ansKey.TryGetValue(s, out value)) {
                        newAns.GetComponent<DragDrop>().itemValue = value.ToString();
                    }
                    GameObject newAnsSlot = Instantiate(answeringSlot);
                    newAnsSlot.transform.SetParent(possibleAnswerView.transform);
                    newAnsSlot.transform.localScale = new Vector3(2f, 2f, 2f);
                }
            }
        }
        if (qObj.questionType != 0) {
            foreach (KeyValuePair<string, int> entry in ansKey) {
                GameObject newAnsSlot = Instantiate(answeringSlot);
                newAnsSlot.transform.SetParent(possibleAnswerView.transform);
                newAnsSlot.transform.localScale = new Vector3(2f, 2f, 2f);
            }
        }
        if (qObj.qImage.Length > 0) {
            if (qObj.qImage.Contains("http")) {
                GameObject newAnsSlot = Instantiate(answeringSlot);
                StartCoroutine(LoadImageCoroutine(qObj.qImage, newAnsSlot));
                newAnsSlot.transform.SetParent(possibleAnswerView.transform);
                newAnsSlot.transform.localScale = new Vector3(2f, 2f, 2f);
                newAnsSlot.GetComponent<Slot>().isImg = true;
            }
        }
    }

    private void solved() {
        GlobalVariables.instance.questionsInMap--;
        if (winObj != null) {
            winObj.SetActive(!winObj.activeSelf);
        }
        Destroy(gameObject.transform.parent.gameObject);
    }

    public void attemptSolution() {
        bool isCorrect = true;
        if (qObj.questionType == 0) {
            for (int i = 0; i < possibleAnswerView.transform.childCount; i++) {
                GameObject child = possibleAnswerView.transform.GetChild(i).gameObject;
                if (System.Int32.Parse(child.GetComponent<Slot>().value) != i) {
                    isCorrect = false;
                }
            }
        } else {
            for (int i = 0; i < possibleAnswerView.transform.childCount; i++) {
                if (isCorrect) {
                    GameObject child = possibleAnswerView.transform.GetChild(i).gameObject;
                    bool checker = false;
                    if (!child.GetComponent<Slot>().isImg) {
                        foreach (KeyValuePair<string, int> entry in ansKey) {
                            if (System.Int32.Parse(child.GetComponent<Slot>().value) == entry.Value) {
                                checker = true;
                            }
                        }
                        if (!checker) {
                            isCorrect = false;
                        }
                    }
                }
            }
        }
        if (isCorrect) {
            solved();
        } else {
            //Incorrect
        }
    }
    public IEnumerator disableScrollView() {
        yield return new WaitForSeconds(1);
        answerView.GetComponent<HorizontalLayoutGroup>().enabled = false;
    }
    public IEnumerator LoadImageCoroutine(string url, GameObject gameObject) {
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

        gameObject.GetComponent<Image>().sprite = sprite;
        if (gameObject.GetComponentInChildren<TextMeshProUGUI>() != null) {
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    public static List<T> Shuffle<T>(List<T> array) {
        int count = array.Count;
        for (int i = count - 1; i > 0; --i) {
            int randIndex = Random.Range(0, i);

            T temp = array[i];
            array[i] = array[randIndex];
            array[randIndex] = temp;
        }
        return array;
    }
}
