using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerCheck : MonoBehaviour
{
    public GameObject firstSlot;
    public GameObject secondSlot;
    public GameObject questionText;
    public Data question;
    // Start is called before the first frame update
    void Awake()
    {
        questionText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (question.subject == "Math")
        {
            if (int.Parse(firstSlot.GetComponent<Slot>().value) + int.Parse(secondSlot.GetComponent<Slot>().value) == int.Parse(question.answer))
            {
                questionText.SetActive(true);
                Debug.Log("correct");
            }
        }
        if(question.subject == "English")
        {
            if (firstSlot.GetComponent<Slot>().value + secondSlot.GetComponent<Slot>().value == question.answer)
            {
                questionText.SetActive(true);
                Debug.Log("correct");
            }
        }
    }
}
