using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Questions/New Question")]

public class Data : ScriptableObject
{
    public int levelNumber;
    public int questionID;
    public string subject;
    public string question1;
    public string question2;
    public string answer;
}
