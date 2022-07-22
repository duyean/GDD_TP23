using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
public class AnswerCheck : MonoBehaviour
{
    public GameObject coin;
    public GameObject firstSlot;
    public GameObject secondSlot;
    public GameObject thirdSlot;
    public TextMeshProUGUI correctWrongText;
    public QuestionCounter questionCounter;
    public QuestionLoader questionLoader;
    public Data question;
    public bool correct;

    //To do:
    //Make slots an array instead of hard coded
    [SerializeField]
    GameObject[] tileMapSpawns;

    [SerializeField]
    GameObject treasure;



    // Start is called before the first frame update
    void Start()
    {
        //correctWrongText.SetActive(true);

        correct = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (firstSlot.GetComponent<Slot>().value != question.finalAnswers[0])
        {
            firstSlot.GetComponent<Slot>().value = null;
        }
        if (secondSlot.GetComponent<Slot>().value != question.finalAnswers[1])
        {
            secondSlot.GetComponent<Slot>().value = null;
        }
        if (thirdSlot.GetComponent<Slot>().value != question.finalAnswers[2])
        {
            thirdSlot.GetComponent<Slot>().value = null;
        }

        
        //If statements to check if the item values add up to the final answer
        //int.Parse is to convert the parameter value in the brackets into int
        if (question.subject == "Math")
        {

            //if (int.Parse(firstSlot.GetComponent<Slot>().value) + int.Parse(secondSlot.GetComponent<Slot>().value) == int.Parse(question.finalAnswer))
            //{
            //    questionText.SetActive(true);
            //    questionLoader.question++;
            //    correct = true;
            //}
            //else if (int.Parse(firstSlot.GetComponent<Slot>().value) + int.Parse(secondSlot.GetComponent<Slot>().value) != int.Parse(question.finalAnswer))
            //{
            //    correct = false;
            //}
            if (firstSlot.GetComponent<Slot>().value == question.finalAnswers[0] && secondSlot.GetComponent<Slot>().value == question.finalAnswers[1] && thirdSlot.GetComponent<Slot>().value == question.finalAnswers[2])
            {
                correctWrongText.SetActive(true);
                spawnPlatform(questionLoader.question);
                questionLoader.question++;
                correct = true;
                
            }
            else
            {
                correct = false;
            }
        }
        if(question.subject == "English")
        {
            //if (firstSlot.GetComponent<Slot>().value + secondSlot.GetComponent<Slot>().value == question.finalAnswer)
            //{
            //    questionText.SetActive(true);
            //    questionLoader.question++;
            //    correct = true;
            //}
            //else if (firstSlot.GetComponent<Slot>().value + secondSlot.GetComponent<Slot>().value != question.finalAnswer)
            //{
            //    correct = false;
            //}
            if (firstSlot.GetComponent<Slot>().value == question.finalAnswers[0] && secondSlot.GetComponent<Slot>().value == question.finalAnswers[1] && thirdSlot.GetComponent<Slot>().value == question.finalAnswers[2])
            {
                correctWrongText.SetActive(true);
                spawnPlatform(questionLoader.question);
                questionLoader.question++;
                correct = true;

            }
            else
            {
                correct = false;
            }
        }

        if (correct)
        {
            coin.SetActive(false);
            questionCounter.answerToFinish--;
        }
    }



    //functions to add
    public void spawnPlatform(int questionNum)
    {
        tileMapSpawns[questionNum - 1].SetActive(true);
    }

    public void checkWin(int level, int question)
    {
        if (question - level == -1)
        {
            treasure.GetComponent<levelWin>().win = true;
        }
    }
}
