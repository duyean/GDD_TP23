using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;

public class connectFirebase : MonoBehaviour {
    // Start is called before the first frame update
    public List<QuestionsObject.QuestionSubject> subjectList = new List<QuestionsObject.QuestionSubject>();
    public QuestionsObject[] mathQuestions = new QuestionsObject[15];
    public QuestionsObject[] englishQuestions = new QuestionsObject[15];
    public QuestionsObject[] chineseQuestions = new QuestionsObject[15];
    FirebaseApp app;
    FirebaseFirestore db;
    public static connectFirebase instance;
    void Start() {

        if (connectFirebase.instance != null) {
            Destroy(connectFirebase.instance);
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available) {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = FirebaseApp.DefaultInstance;
                db = FirebaseFirestore.DefaultInstance;
                loadQuestion();
                // Set a flag here to indicate whether Firebase is ready to use by your app.
            } else {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    void loadQuestion() {
        for (int i = 0; i < 3; i++) {
            QuestionsObject.QuestionSubject sub = (QuestionsObject.QuestionSubject)i;
            subjectList.Add(sub);
            CollectionReference questionsRef = db.Collection(sub.ToString().ToLower());
            questionsRef.GetSnapshotAsync().ContinueWithOnMainThread(task => {
                QuerySnapshot snapshot = task.Result;
                int questionNo = 0;
                foreach (DocumentSnapshot document in snapshot.Documents) {
                    int questionid = Convert.ToInt32(document.Id);
                    Dictionary<string, object> questionList = document.ToDictionary();
                    List<string> answerSlots = new List<string>();
                    List<string> answers = new List<string>();
                    List<string> finalAnswer = new List<string>();
                    foreach (System.Object a in questionList["answerSlots"] as List<System.Object>) {
                        answerSlots.Add((string)a);
                    }
                    foreach (System.Object a in questionList["answers"] as List<System.Object>) {
                        answers.Add((string)a);
                    }
                    foreach (System.Object a in questionList["finalAnswer"] as List<System.Object>) {
                        finalAnswer.Add((string)a);
                    }
                    string hint = questionList["hint"].ToString();
                    string question = questionList["question"].ToString();
                    string prefix = questionList["prefix"].ToString();
                    string postfix = questionList["postfix"].ToString();
                    int questionType = Convert.ToInt32(questionList["questionType"]);
                    string qImage = questionList["qImage"].ToString();
                    switch (sub) {
                        case QuestionsObject.QuestionSubject.Math:
                            mathQuestions[questionNo] = (new QuestionsObject(sub, answerSlots, answers, finalAnswer, hint, question, questionid, prefix, postfix, questionType, qImage));
                            break;
                        case QuestionsObject.QuestionSubject.English:
                            englishQuestions[questionNo] = (new QuestionsObject(sub, answerSlots, answers, finalAnswer, hint, question, questionid, prefix, postfix, questionType, qImage));
                            break;
                        case QuestionsObject.QuestionSubject.Chinese:
                            chineseQuestions[questionNo] = (new QuestionsObject(sub, answerSlots, answers, finalAnswer, hint, question, questionid, prefix, postfix, questionType, qImage));
                            break;
                    }
                    questionNo += 1;
                }
            });
        }
    }
}
