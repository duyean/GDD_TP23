using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionLoader : MonoBehaviour
{
    public AnswerCheck answerCheck;
    public int level = 1;
    public int question = 1;
    public GameObject item;
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        //load questions based on parameters set in inspector
        answerCheck.question = (Data)Resources.Load("Questions/" + level + "_" + question);
        SpawnAnswers(question);
    }

    // Update is called once per frame
    void Update()
    {
        if (answerCheck.question.finalLevel)
        {
            level++;
        }
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
