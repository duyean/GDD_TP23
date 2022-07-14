using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerCheck : MonoBehaviour
{
    public GameObject firstSlot;
    public GameObject secondSlot;
    public GameObject questionText;
    // Start is called before the first frame update
    void Awake()
    {
        questionText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (firstSlot.GetComponent<Slot>().firstValue + secondSlot.GetComponent<Slot2>().secondValue == 3)
        {
            questionText.SetActive(true);
            Debug.Log("correct");
        }
    }
}
