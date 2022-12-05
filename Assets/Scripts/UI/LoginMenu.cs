using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoginMenu : MonoBehaviour {
    public Image[] tabs;
    public GameObject[] switchMenuItem;
    public TMP_InputField[] loginDetails;
    private int loginType = 0;
    public static LoginMenu instance;
    private void Start() {
        if (LoginMenu.instance != null) {
            Destroy(LoginMenu.instance);
        }
        instance = this;
        if (GlobalVariables.instance.loginStatus) {
            switchMenu();
        }
    }
    public void switchTab(int type) {
        loginType = type;
        switch (type) {
            case 0:
                tabs[0].color = new Color(242f / 255f, 243f / 255f, 195f / 255f);
                tabs[1].color = new Color(242f / 255f, 243f / 255f, 170f / 255f);
                //loginDetails[0].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "User ID";
                break;
            case 1:
                tabs[1].color = new Color(242f / 255f, 243f / 255f, 195f / 255f);
                tabs[0].color = new Color(242f / 255f, 243f / 255f, 170f / 255f);
                //loginDetails[0].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Email";
                break;
        }
    }

    public void login() {
        //Override
        if (loginDetails[0].text == "admin" && loginDetails[1].text == "admin") {
            GlobalVariables.instance.loginStatus = true;
            switchMenu();
        }
        switch (loginType) {
            case 0:
                //Student Login
                break;
            case 1:
                //Teacher Login
                break;
        }
    }
    private void switchMenu() {
        if (GlobalVariables.instance.isTeacher) {
            //Enable teacherStuff
        }
        switchMenuItem[0].SetActive(true);
        switchMenuItem[1].SetActive(false);
    }

}
