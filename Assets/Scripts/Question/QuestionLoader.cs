using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionLoader : MonoBehaviour
{
    public AnswerCheck answerCheck;
    public int level;
    public int question;
    public GameObject item;
    public Canvas canvas;
    public GameObject[] coins;
    private Coin coin;

    void Awake()
    {
        coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject i in coins)
        {
            coin = i.GetComponent<Coin>();
        }
        level = coin.level;
        question = coin.questionNumber;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(level + question);
        //load questions based on parameters set by coin
        answerCheck.question = (Data)Resources.Load("Questions/" + level + "_" + question);
        SpawnAnswers(question);
    }

    // Update is called once per frame
    void Update()
    {
    }

    //Instantiate item prefab with answer values from specified scriptable object
    void SpawnAnswers(int level)
    {
        if (answerCheck.question.questionID == level)
        {
            foreach (string s in answerCheck.question.answers)
            {
                item.GetComponent<DragDrop>().itemValue = s;
                Instantiate(item).transform.parent = canvas.transform;
                item.GetComponent<DragDrop>().canvas = canvas;
            }
        }
    }
}
