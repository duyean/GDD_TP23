using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour {
    public bool loginStatus;
    public bool isTeacher;
    public static GlobalVariables instance;
    public int questionsInMap;
    private void Start() {
        if (GlobalVariables.instance != null) {
            Destroy(GlobalVariables.instance);
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
}
