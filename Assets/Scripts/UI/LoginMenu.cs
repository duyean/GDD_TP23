using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LoginMenu : MonoBehaviour {
    public Image[] tabs;
    public Sprite[] swapTabs;
    public GameObject[] switchMenuItem, teacherView;
    public TMP_InputField[] loginDetails;
    public TextMeshProUGUI errorMsg;
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
                tabs[0].sprite = swapTabs[0];
                tabs[1].sprite = swapTabs[2];
                //loginDetails[0].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "User ID";
                break;
            case 1:
                tabs[0].sprite = swapTabs[1];
                tabs[1].sprite = swapTabs[3];
                //loginDetails[0].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Email";
                break;
        }
    }

    public void login() {
        errorMsg.text = "";
        //Override
        if (loginDetails[0].text == "admin" && loginDetails[1].text == "admin") {
            if (loginType == 1) {
                GlobalVariables.instance.adminMode = true;
                GlobalVariables.instance.isTeacher = true;
            }
            GlobalVariables.instance.loginStatus = true;
            switchMenu();
        } else {
            switch (loginType) {
                case 0: //Student
                    StartCoroutine(loginToDatabase(false, loginDetails[0].text, loginDetails[1].text));
                    break;
                case 1://Teacher
                    StartCoroutine(loginToDatabase(true, loginDetails[0].text, loginDetails[1].text));
                    break;
            }
        }
    }
    private void switchMenu() {
        if (GlobalVariables.instance.isTeacher) {
            foreach (GameObject i in teacherView) {
                if (i.name != "TeacherTool") {
                    i.SetActive(true);
                } else {
                    if (GlobalVariables.instance.adminMode) {
                        i.SetActive(true);
                    }
                }

            }
        }
        switchMenuItem[0].SetActive(true);
        switchMenuItem[1].SetActive(false);
    }

    IEnumerator loginToDatabase(bool isTeacherLogin, string username, string password) {
        WWWForm form = new WWWForm();
        form.AddField("action", "login");
        form.AddField("username", username);
        form.AddField("password", password);
        if (isTeacherLogin) {
            using (UnityWebRequest www = UnityWebRequest.Post("https://eproject-dev.ednoland.com/matp/teacherApp/teacherAppRequest.php", form)) {
                www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
                www.SetRequestHeader("Access-Control-Allow-Credentials", "true");
                www.SetRequestHeader("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
                www.SetRequestHeader("Access-Control-Allow-Methods", "POST");
                www.SetRequestHeader("Access-Control-Allow-Origin", "*");
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success) {
                    Debug.Log(www.error);
                } else {
                    string[] returndata = (www.downloadHandler.text).Split(",");
                    if (returndata.Length > 0) {
                        foreach (string i in returndata) {
                            if (i.Equals("\"role_id\":\"3\"")) {
                                GlobalVariables.instance.adminMode = true;
                            }
                        }
                        foreach (string i in returndata) {
                            if (i.Contains("result") && i.Substring(i.Length - 3).Equals("\"1\"")) {
                                GlobalVariables.instance.isTeacher = true;
                                GlobalVariables.instance.loginStatus = true;
                                switchMenu();
                                break;
                            }
                        }
                    }
                }
            }
        } else {
            using (UnityWebRequest www = UnityWebRequest.Post("https://eproject-dev.ednoland.com/matp/parentAppV2/parentAppRequest.php", form)) {
                www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
                www.SetRequestHeader("Access-Control-Allow-Credentials", "true");
                www.SetRequestHeader("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
                www.SetRequestHeader("Access-Control-Allow-Methods", "POST");
                www.SetRequestHeader("Access-Control-Allow-Origin", "*");
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success) {
                    Debug.Log(www.error);
                } else {
                    string[] returndata = (www.downloadHandler.text).Split(",");
                    if (returndata.Length > 0) {
                        foreach (string i in returndata) {
                            if (i.Contains("reason") && i.Contains("success")) {
                                GlobalVariables.instance.loginStatus = true;
                                switchMenu();
                                break;
                            }
                        }
                    }
                }
            }
        }
        if (!GlobalVariables.instance.loginStatus) {
            errorMsg.text = "Incorrect user ID or password";
        }
    }
}
