using UnityEngine;
using UnityEditor;
using System.IO;

//tutorial from Comp-3 Interactive (https://www.youtube.com/watch?v=1EdLTF43d70)
public class CSV_Import
{
    private static string questionCSVPath = "/Resources/CSV/Questions.csv";

    //Add tab to generate questions in the Unity Editor
    [MenuItem("Utilities/Generate Questions")]
    public static void GenerateQuestions()
    {
        //add csv lines to array
        string[] allLines = File.ReadAllLines(Application.dataPath + questionCSVPath);

        //split lines by ",", then answers by "|"
        foreach(string s in allLines)
        {
            string[] splitData = s.Split(',');
            string[] splitAnswerData = splitData[4].Split('|');
            string[] splitFilledValueData = splitData[5].Split('|');
            string[] splitFinalAnswerData = splitData[6].Split('|');
            Data data = ScriptableObject.CreateInstance<Data>();
            data.subject = splitData[0];
            data.levelNumber = int.Parse(splitData[1]);
            data.questionID = int.Parse(splitData[2]);
            data.question = splitData[3];
            data.hint = splitData[7];
            data.solution = splitData[8];
            foreach(string i in splitAnswerData)
            {
                data.answers.Add(i);
            }
            foreach (string i in splitFilledValueData)
            {
                data.filledValues.Add(i);
            }
            foreach (string i in splitFinalAnswerData)
            {
                data.finalAnswers.Add(i);
            }
            //create the Scriptable Object
            AssetDatabase.CreateAsset(data,$"Assets/Resources/Questions/{data.subject + "_" + data.levelNumber + "_" + data.questionID}.asset");
        }

        AssetDatabase.SaveAssets();
    }
}
