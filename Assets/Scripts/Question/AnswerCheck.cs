using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
public class AnswerCheck : MonoBehaviour
{

    public GameObject firstSlot;
    public GameObject secondSlot;
    public GameObject questionText;
    public QuestionLoader questionLoader;
    public Data question;
    private int numberOfQuestions;

    // Start is called before the first frame update
    void Awake()
    {
        questionText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //If statements to check if the item values add up to the final answer
        //int.Parse is to convert the parameter value in the brackets into int
        if (question.subject == "Math")
        {
            if (int.Parse(firstSlot.GetComponent<Slot>().value) + int.Parse(secondSlot.GetComponent<Slot>().value) == int.Parse(question.finalAnswer))
            {
                questionText.SetActive(true);
                questionLoader.question++;
                Debug.Log("correct");
            }
            else if (int.Parse(firstSlot.GetComponent<Slot>().value) + int.Parse(secondSlot.GetComponent<Slot>().value) != int.Parse(question.finalAnswer))
            {
                Debug.Log("wrong");
            }
        }
        if(question.subject == "English")
        {
            if (firstSlot.GetComponent<Slot>().value + secondSlot.GetComponent<Slot>().value == question.finalAnswer)
            {
                questionText.SetActive(true);
                questionLoader.question++;
                Debug.Log("correct");
            }
            else if (firstSlot.GetComponent<Slot>().value + secondSlot.GetComponent<Slot>().value != question.finalAnswer)
            {
                Debug.Log("wrong");

            }
        }
        if (question.finalLevel)
        {

        }
    }
}
