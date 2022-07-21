using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionCounter : MonoBehaviour
{
    public Coin[] coins;
    public List<GameObject> questions;
    public int answerToFinish;
    private List<QuestionLoader> questionLoaders;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject gameObject in questions)
        {
            questionLoaders.Add(gameObject.GetComponent<QuestionLoader>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < coins.Length; i++)
        {
            if (!coins[i].enabled)
            {
                checkCorrect();
            }
        }
    }

    void checkCorrect()
    {
        foreach (QuestionLoader questionLoader in questionLoaders)
        {
            if (questionLoader.answerCheck.correct)
            {
                answerToFinish--;
            }
        }
    }
}
