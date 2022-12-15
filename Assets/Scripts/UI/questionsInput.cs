using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class questionsInput : MonoBehaviour {
    public GameObject[] checkbox;
    public ToggleGroup toggleGroup;
    public void changeQnsType(TMP_Dropdown dd) {
        switch (dd.value) {
            case 0:
                foreach (GameObject i in checkbox) {
                    if (!i.activeSelf) {
                        i.SetActive(true);
                    }
                }
                toggleGroup.enabled = true;
                break;
            case 1:
                foreach (GameObject i in checkbox) {
                    if (!i.activeSelf) {
                        i.SetActive(true);
                    }
                }
                toggleGroup.enabled = false;
                break;
            case 2:
                foreach (GameObject i in checkbox) {
                    if (i.activeSelf) {
                        i.SetActive(false);
                    }
                }
                toggleGroup.enabled = false;
                break;
        }
    }
}
