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
<<<<<<< Updated upstream
=======
    public TextMeshProUGUI displayAnswerText;
>>>>>>> Stashed changes

    // Start is called before the first frame update
    void Awake()
    {
<<<<<<< Updated upstream
=======
        displayAnswerText = GetComponentInChildren<TextMeshProUGUI>();
>>>>>>> Stashed changes
        questionText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //If statements to check if the item values add up to the final answer
        //int.Parse is to convert the parameter value in the brackets into int
        if (question.subject == "Math")
        {
            displayAnswerText.SetText(question.finalAnswer); //Displaying the answer in the last box
            if(firstSlot.GetComponent<Slot>().value != null && secondSlot.GetComponent<Slot>().value != null)
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
            else
            {
                Debug.Log("checking");
            }
        }
        if(question.subject == "English")
        {
            displayAnswerText.SetText(question.finalAnswer); //Displaying the answer in the last box
            if (firstSlot.GetComponent<Slot>().value != null && secondSlot.GetComponent<Slot>().value != null)
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
            else
            {
                    Debug.Log("checking");
            }
                
        }
        if (question.finalLevel)
        {

        }
    }
}
