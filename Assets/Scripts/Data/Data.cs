using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Questions/New Question")]

public class Data : ScriptableObject
{
    public string subject;
    public int levelNumber;
    public int questionID;
    public string question;
    public List<string> answers = new List<string>();
    public List<string> filledValues = new List<string>();
    public List<string> finalAnswers = new List<string>();
    public string hint;
    public string solution;
}
