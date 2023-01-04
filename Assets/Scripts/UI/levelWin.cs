using UnityEngine;

public class levelWin : MonoBehaviour {
    public GameObject treasure;
    public bool win = false;
    private void Update() {
        win = (GlobalVariables.instance.questionsInMap == 0);
    }
    private void OnTriggerEnter2D(Collider2D collider) {
        if (win) {
            Destroy(treasure);
            WinUI.Instance.winUI.SetActive(true);
        }

    }
}
