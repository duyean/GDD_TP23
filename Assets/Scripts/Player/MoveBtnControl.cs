using UnityEngine;
using UnityEngine.EventSystems;

public class MoveBtnControl : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler {
    GameObject player;
    private enum buttonType { Left, Right, Jump };
    private buttonType currentBtn;
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        if (transform.name.Contains("Jump")) {
            currentBtn = buttonType.Jump;
        } else if (transform.name.Contains("Left")) {
            currentBtn = buttonType.Left;
        } else if (transform.name.Contains("Right")) { currentBtn = buttonType.Right; }

    }
    public void OnPointerEnter(PointerEventData eventData) {
        if (currentBtn == buttonType.Jump) {
            player.GetComponent<MovementScript>().Jump();
        }
    }
    public void OnPointerDown(PointerEventData eventData) {
        if (currentBtn == buttonType.Left) {
            player.GetComponent<MovementScript>().moveBtn = -1;
        } else if (currentBtn == buttonType.Right) {
            player.GetComponent<MovementScript>().moveBtn = 1;
        } else if (currentBtn == buttonType.Jump) {
            player.GetComponent<MovementScript>().Jump();
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (currentBtn == buttonType.Left || currentBtn == buttonType.Right) {
            player.GetComponent<MovementScript>().moveBtn = 0;
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (currentBtn == buttonType.Left || currentBtn == buttonType.Right) {
            player.GetComponent<MovementScript>().moveBtn = 0;
        }
    }
}
