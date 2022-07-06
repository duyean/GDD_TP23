using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Questions")]
public class Questions : ScriptableObject
{
    public string subject;
    public string question;
    public string[] answers;
    public int correct_answer;
}
