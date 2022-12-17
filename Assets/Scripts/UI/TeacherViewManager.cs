using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherViewManager : MonoBehaviour {
    public GameObject scrollviewObj, subjectScrollView, questionScrollView, mainTeacherUI;
    public GameObject[] teacherViewMenu;
    public void switchUI(int id) {
        if (!mainTeacherUI.activeSelf) {
            mainTeacherUI.SetActive(true);
        }
        switch (id) {
            case 0:
                for (int i = 0; i < subjectScrollView.transform.childCount; i++) {
                    Destroy(subjectScrollView.transform.GetChild(0));
                }
                for (int i = 0; i < 3; i++) {
                    Instantiate(scrollviewObj, subjectScrollView.transform);
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
                for (int i = 0; i < 15; i++) {
                    Instantiate(scrollviewObj, questionScrollView.transform);
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
}
