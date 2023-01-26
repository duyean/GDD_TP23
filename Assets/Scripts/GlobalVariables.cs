using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GlobalVariables : MonoBehaviour {
    public bool loginStatus, isTeacher, adminMode;
    public static GlobalVariables instance;
    public int questionsInMap;
    public TMP_FontAsset chineseFont, englishFont;
    public GameObject debugObj;
    private void Start() {
        if (GlobalVariables.instance != null) {
            Destroy(GlobalVariables.instance);
        }
        instance = this;
        DontDestroyOnLoad(this);

        if (!adminMode) {
            debugObj.SetActive(false);
        }
    }
}
