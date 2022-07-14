using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Questions/New Question")]

public class Data : ScriptableObject
{
    public int levelNumber;
    public string subject;
    public string question;
    public string answer;
}
