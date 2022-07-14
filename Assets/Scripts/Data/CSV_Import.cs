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
            data.subject = splitData[1];
            data.question = splitData[2];
            data.answer = splitData[3];

            AssetDatabase.CreateAsset(data,$"Assets/Questions/{data.question}.asset");
        }

        AssetDatabase.SaveAssets();
    }
}
