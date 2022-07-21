using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnQuestionAnswered : MonoBehaviour
{
    [SerializeField]
    GameObject[] tileMapSpawns;

    public void onAnswerQuestion(int questionNum)
    {
        tileMapSpawns[questionNum - 1].SetActive(true);
    }
}
