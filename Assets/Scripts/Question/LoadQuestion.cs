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

    private QuestionsObject qObj;
    private void Start() {
        GlobalVariables.instance.questionsInMap++;
        loadQuestion();
    }

    public void loadQuestion() {
        //int maxQid = Mathf.RoundToInt(0.5f * level * (level + 1));
        //maxQid = (maxQid - GlobalVariables.instance.questionsInMap) + 1;
        int maxQid = 0;
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
                newAns.GetComponent<DragDrop>().itemValue = i.ToString();

                if (qObj.questionType == 0) {
                    GameObject newAnsSlot = Instantiate(answeringSlot);
                    newAnsSlot.transform.SetParent(possibleAnswerView.transform);
                    newAnsSlot.transform.localScale = new Vector3(2f, 2f, 2f);
                }
            }
            i++;
        }


    }
    private void solved() {
        GlobalVariables.instance.questionsInMap--;
        winObj.SetActive(!winObj.activeSelf);
        Destroy(gameObject.transform.parent.gameObject);
    }
    public void attemptSolution() {

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
