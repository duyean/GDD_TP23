using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeacherViewManager : MonoBehaviour {
    public GameObject scrollviewObj, subjectScrollView, questionScrollView, mainTeacherUI;
    public GameObject[] teacherViewMenu;
    private connectFirebase firebaseObj;
    public Sprite[] subjectIcon;
    private void Start() {
        firebaseObj = connectFirebase.instance;
    }
    public void switchUI(int id) {
        if (!mainTeacherUI.activeSelf) {
            mainTeacherUI.SetActive(true);
        }
        switch (id) {
            case 0:
                for (int i = 0; i < subjectScrollView.transform.childCount; i++) {
                    Destroy(subjectScrollView.transform.GetChild(0));
                }
                for (int i = 0; i < firebaseObj.subjectList.Count; i++) {

                    GameObject spawnObj = Instantiate(scrollviewObj, subjectScrollView.transform);
                    //spawnObj.transform.GetChild(0).GetComponent<Image>().sprite = subjectIcon[i];
                    spawnObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = firebaseObj.subjectList[i].ToString();
                    spawnObj.GetComponent<Button>().onClick.AddListener(delegate { spawnQuestionObj(i); });
                }
                foreach (GameObject i in teacherViewMenu) {
                    i.SetActive(false);
                }
                teacherViewMenu[0].SetActive(true);
                break;

            case 1:
                for (int i = 0; i < questionScrollView.transform.childCount; i++) {
                    Destroy(questionScrollView.transform.GetChild(0));
                }
                foreach (GameObject i in teacherViewMenu) {
                    i.SetActive(false);
                }
                teacherViewMenu[1].SetActive(true);
                break;

            case 2:
                break;
        }
    }
    public void spawnQuestionObj(int sub) {
        switchUI(1);
        int level = 1;
        int questionNo = 1;
        for (int i = 0; i < 15; i++) {
            GameObject spawnObj = Instantiate(scrollviewObj, questionScrollView.transform);
            //spawnObj.transform.GetChild(0).GetComponent<Image>().sprite = subjectIcon[sub];
            spawnObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Level " + level + " Question " + questionNo;
            questionNo++;
            if (questionNo > level) {
                questionNo = 1;
                level++;
            }
        }
    }
}
