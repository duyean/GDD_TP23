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

    // Start is called before the first frame update
    void Start()
    {
        questionText.SetText(question.question);
        for(int i = 0; i < question.answers.Length; i++)
        {
            answerButton[i].SetActive(true);
            if(i == question.correct_answer)
            {
                //answerButton[i].GetComponent<Button>().onClick.AddListener(correctAnswer());
            }
        }
    }

    //UnityAction correctAnswer()
    //{
    //    Debug.Log("Correct");
    //    return;
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
