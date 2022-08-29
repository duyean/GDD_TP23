using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public string slotName;
    public string value;
    public AnswerCheck answerCheck;
    public string itemIn; 

    public void Start()
    {
        answerCheck = gameObject.GetComponentInParent<AnswerCheck>(); //Get the AnswerCheck script from the parent of the current GameObject
    }

    //Detects when an item has been let go of
    public void OnDrop(PointerEventData eventData) 
    {
        //See if there is an item being dragged
        if (eventData.pointerDrag != null)
        {
            //Anchors the item to the slot when it is dropped on the slot
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            //Gets the itemValue from the object anchored to the slot and parses into the value variable
            value = eventData.pointerDrag.GetComponent<DragDrop>().itemValue;
            ////Gets the itemIn from the object and parses into the value variable, this is to debug the slot not getting values anymore
            itemIn = eventData.pointerDrag.GetComponent<DragDrop>().itemValue;
            //Adds the value into the AnswerCheck script to check if it is correct
            answerCheck.AddValues(gameObject.GetComponent<Slot>().slotName, value);
        }
    }

    //Reset slot value when item is dragged out
    private void OnTriggerExit2D(Collider2D collision)
    {
        answerCheck.RemoveValues(gameObject.GetComponent<Slot>().slotName);
        value = null;       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //To debug the slot no longer getting values
        value = itemIn;
    }
}
