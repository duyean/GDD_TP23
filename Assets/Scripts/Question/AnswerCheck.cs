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
    //public GameObject player;
    //private Rigidbody2D rb2d;

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
        //rb2d = player.GetComponent<Rigidbody2D>();
        correct = false;
    }
    
    // Update is called once per frame
    void Update()
    {
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
                //correctWrongText.SetActive(true);
                Debug.Log(questionLoader.question);
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
                //correctWrongText.SetActive(true);
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
            //UnFreeze();
            coin.SetActive(false);
            questionCounter.answerToFinish--;
            checkWin(questionCounter.answerToFinish);
        }
    }

    //functions to add
    public void spawnPlatform(int questionNum)
    {
        tileMapSpawns[questionNum - 1].SetActive(true);
    }

    public void checkWin(int answerToFinish)
    {
        if (answerToFinish == 0)
        {
            treasure.GetComponent<levelWin>().win = true;
        }
    }
   
    //void UnFreeze()
    //{
    //    rb2d.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
    //}

}
