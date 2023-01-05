using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler {
    public string slotId;
    public string value;
    public AnswerCheck answerCheck;
    public string itemIn, slotName;
    public bool isImg = false;

    public void Start() {
        //answerCheck = gameObject.GetComponentInParent<AnswerCheck>(); //Get the AnswerCheck script from the parent of the current GameObject
    }

    //Detects when an item has been let go of
    public void OnDrop(PointerEventData eventData) {
        if (!isImg) {
            //See if there is an item being dragged
            if (eventData.pointerDrag != null) {
                eventData.pointerDrag.transform.position = gameObject.transform.position;
                //Anchors the item to the slot when it is dropped on the slot
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                //Re-enabling the collider of the dragged item after it has been dropped to a slot. Disabling and re-enabling of the collider is to help debug the slot retaining their values even after the item is no longer inside
                eventData.pointerDrag.GetComponent<BoxCollider2D>().enabled = true;
                //Gets the itemValue from the object anchored to the slot and parses into the value variable
                value = eventData.pointerDrag.GetComponent<DragDrop>().itemValue;
                //Gets the itemIn from the object and parses into the value variable, this is to debug the slot not getting values anymore
                itemIn = eventData.pointerDrag.GetComponent<DragDrop>().itemValue;
                //Adds the value into the AnswerCheck script to check if it is correct
                //answerCheck.AddValues(gameObject.GetComponent<Slot>().slotName, value);
            }
        }
    }

    //Reset slot value when item is dragged out
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Item" && !isImg) {
            //answerCheck.RemoveValues(gameObject.GetComponent<Slot>().slotName);
            value = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Item" && !isImg) {
            //To fix the slot no longer getting values. For reasons unknown, without this function, the slot can no longer receive values
            value = itemIn;
        }
    }
}
