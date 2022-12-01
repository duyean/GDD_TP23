using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;

public class connectFirebase : MonoBehaviour {
    // Start is called before the first frame update
    public List<QuestionsObject> questions = new List<QuestionsObject>();
    FirebaseApp app;
    FirebaseFirestore db;
    public bool load;
    void Start() {
        DontDestroyOnLoad(gameObject);
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available) {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = FirebaseApp.DefaultInstance;
                db = FirebaseFirestore.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            } else {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });

    }
    private void Update() {
        if (load) {
            load = false;
            loadQuestion();
        }
    }
    void loadQuestion() {
        CollectionReference questionsRef = db.Collection("questions");
        questionsRef.GetSnapshotAsync().ContinueWithOnMainThread(task => {
            QuerySnapshot snapshot = task.Result;

            foreach (DocumentSnapshot document in snapshot.Documents) {
                Dictionary<string, object> questionList = document.ToDictionary();

                QuestionsObject.QuestionSubject sub = QuestionsObject.QuestionSubject.none;
                switch (questionList["Subject"].ToString().ToLower()) {
                    case "english":
                        sub = QuestionsObject.QuestionSubject.English;
                        break;
                    case "math":
                        sub = QuestionsObject.QuestionSubject.Math;
                        break;
                    case "science":
                        sub = QuestionsObject.QuestionSubject.Science;
                        break;
                    case "chinese":
                        sub = QuestionsObject.QuestionSubject.Chinese;
                        break;
                    case "malay":
                        sub = QuestionsObject.QuestionSubject.Malay;
                        break;
                }
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
                int questionid = Convert.ToInt32(questionList["questionId"].ToString());
                questions.Add(new QuestionsObject(sub, answerSlots, answers, finalAnswer, hint, question, questionid));
                print("added");
            }
            print(questions.Count);
        });
    }
}
