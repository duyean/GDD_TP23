using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;

public class connectFirebase : MonoBehaviour {
    // Start is called before the first frame update
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
        CollectionReference usersRef = db.Collection("users");
        usersRef.GetSnapshotAsync().ContinueWithOnMainThread(task => {
            QuerySnapshot snapshot = task.Result;
            foreach (DocumentSnapshot document in snapshot.Documents) {
                Debug.Log(document.Id);
            }

            Debug.Log("Read all data from the users collection.");
        });

    }

}
