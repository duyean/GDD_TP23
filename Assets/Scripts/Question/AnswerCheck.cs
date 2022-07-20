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
    public TextMeshProUGUI displayAnswerText;
    public bool correct;

    // Start is called before the first frame update
    void Start()
    {
        displayAnswerText.GetComponentInChildren<TextMeshProUGUI>();
        questionText.SetActive(false);

        correct = false;
    }

    // Update is called once per frame
    void Update()
    {

        //If statements to check if the item values add up to the final answer
        //int.Parse is to convert the parameter value in the brackets into int
        if (question.subject == "Math")
        {
            displayAnswerText.SetText(question.finalAnswer);

            if (int.Parse(firstSlot.GetComponent<Slot>().value) + int.Parse(secondSlot.GetComponent<Slot>().value) == int.Parse(question.finalAnswer))
            {
                questionText.SetActive(true);
                questionLoader.question++;
                correct = true;
            }
            else if (int.Parse(firstSlot.GetComponent<Slot>().value) + int.Parse(secondSlot.GetComponent<Slot>().value) != int.Parse(question.finalAnswer))
            {
                correct = false;
            }
        }
        if(question.subject == "English")
        {
            displayAnswerText.SetText(question.finalAnswer);

            if (firstSlot.GetComponent<Slot>().value + secondSlot.GetComponent<Slot>().value == question.finalAnswer)
            {
                questionText.SetActive(true);
                questionLoader.question++;
                correct = true;
            }
            else if (firstSlot.GetComponent<Slot>().value + secondSlot.GetComponent<Slot>().value != question.finalAnswer)
            {
                correct = false;
            }
        }
        if (question.finalLevel)
        {

        }
    }
}
