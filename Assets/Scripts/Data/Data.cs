using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Questions/New Question")]

public class Data : ScriptableObject
{
    public int levelNumber;
    public int questionID;
    public string subject;
    public List<string> answers = new List<string>();
    public List<string> filledValues = new List<string>();
    public string finalAnswer;
    public bool finalLevel;
}
