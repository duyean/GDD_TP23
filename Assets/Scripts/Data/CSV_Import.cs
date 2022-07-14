using UnityEngine;
using UnityEditor;
using System.IO;

//tutorial from Comp-3 Interactive (https://www.youtube.com/watch?v=1EdLTF43d70)
public class CSV_Import
{
    private static string questionCSVPath = "/Scripts/Data/CSV/Questions.csv";

    [MenuItem("Utilities/Generate Questions")]
    public static void GenerateQuestions()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + questionCSVPath);

        foreach(string s in allLines)
        {
            string[] splitData = s.Split(',');

            Data data = ScriptableObject.CreateInstance<Data>();
            data.levelNumber = int.Parse(splitData[0]);
            data.questionID = int.Parse(splitData[1]);
            data.subject = splitData[2];
            data.question1 = splitData[3];
            data.question2 = splitData[4];
            data.answer = splitData[5];

            AssetDatabase.CreateAsset(data,$"Assets/Resources/Questions/{data.levelNumber + "_" + data.questionID}.asset");
        }

        AssetDatabase.SaveAssets();
    }
}
