using System.IO;
using UnityEditor;
using UnityEngine;

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
        foreach (string s in allLines)
        {
            string[] splitData = s.Split(',');
            Data data = ScriptableObject.CreateInstance<Data>();

            data.answers.Add(splitData[4]);
            data.answers.Add(splitData[5]);
            data.answers.Add(splitData[6]);
            data.answers.Add(splitData[7]);
            data.answers.Add(splitData[8]);

            data.filledValues.Add(splitData[9]);
            data.filledValues.Add(splitData[10]);
            data.filledValues.Add(splitData[11]);
            data.filledValues.Add(splitData[12]);
            data.filledValues.Add(splitData[13]);

            for (int i = 14; i < 19; i++)
            {
                data.finalAnswers.Add(splitData[i]);
            }

            data.subject = splitData[0];
            data.levelNumber = int.Parse(splitData[1]);
            data.questionID = int.Parse(splitData[2]);
            data.question = splitData[3];
            data.hint = splitData[19];
            data.solution = splitData[20];

            //create the Scriptable Object
            AssetDatabase.CreateAsset(data, $"Assets/Resources/Questions/{data.subject + "_" + data.levelNumber + "_" + data.questionID}.asset");
        }

        AssetDatabase.SaveAssets();
    }
}
