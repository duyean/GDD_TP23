using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class DisplayQuestions : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public GameObject[] answerButton;
    public Questions question;
    private UnityAction CorrectAnswer;
    private UnityAction WrongAnswer;

    // Start is called before the first frame update
    void Start()
    {
        CorrectAnswer += correctAnswer;
        WrongAnswer += wrongAnswer;
        questionText.SetText(question.question);
        for(int i = 0; i < question.answers.Length; i++)
        {
            answerButton[i].SetActive(true);
            answerButton[i].GetComponentInChildren<TextMeshProUGUI>().SetText(question.answers[i]);
            if(i == question.correct_answer)
            {
                answerButton[i].GetComponent<Button>().onClick.AddListener(CorrectAnswer);
            }
            else
            {
                answerButton[i].GetComponent<Button>().onClick.AddListener(WrongAnswer);
            }
        }
        if (question.answers.Length < answerButton.Length)
        {
            for (int i = question.answers.Length; i < answerButton.Length; i++)
            {
                answerButton[i].SetActive(false);
            }
        }
    }

    void correctAnswer()
    {
        Debug.Log("Correct");
    }
    void wrongAnswer()
    {
        Debug.Log("Wrong answer, try again");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
