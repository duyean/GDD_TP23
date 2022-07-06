using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
                answerButton[i].onclick.addlistener;
            }
        }
    }

    void correctAnswer()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
