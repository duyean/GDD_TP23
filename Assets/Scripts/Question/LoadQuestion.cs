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

        }
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
                if (ansKey.TryGetValue(s, out value)) {
                    newAns.GetComponent<DragDrop>().itemValue = value.ToString();
                }

                if (qObj.questionType == 0) {
                    GameObject newAnsSlot = Instantiate(answeringSlot);
                    newAnsSlot.transform.SetParent(possibleAnswerView.transform);
                    newAnsSlot.transform.localScale = new Vector3(2f, 2f, 2f);
                }
            }
        }
        //StartCoroutine(disableScrollView());

    }
    private void solved() {
        GlobalVariables.instance.questionsInMap--;
        winObj.SetActive(!winObj.activeSelf);
        Destroy(gameObject.transform.parent.gameObject);
    }
    public void attemptSolution() {
        bool isCorrect = true;
        switch (qObj.questionType) {
            case 0:
                for (int i = 0; i < possibleAnswerView.transform.childCount; i++) {
                    GameObject child = possibleAnswerView.transform.GetChild(i).gameObject;
                    if (System.Int32.Parse(child.GetComponent<Slot>().value) != i) {
                        isCorrect = false;
                    }
                }
                break;
            case 1:
                break;
            case 2:
                break;
        }
        print(isCorrect);
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
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "";
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
