using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public string value;
    public AnswerCheck answerCheck;
    public string itemIn; 

    public void Start()
    {
        answerCheck = gameObject.GetComponentInParent<AnswerCheck>();
    }

    public void OnDrop(PointerEventData eventData) 
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition; //Anchors the item to the slot when it is dropped on the slot
            value = eventData.pointerDrag.GetComponent<DragDrop>().itemValue; //Gets the itemValue from the object and parses into the value variable
            itemIn = eventData.pointerDrag.GetComponent<DragDrop>().itemValue;
            answerCheck.AddValues(gameObject.name,value);
        }
    }

    

    //Reset slot value when item is dragged out
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        answerCheck.RemoveValues(gameObject.name);
        value = null;
        
    }
    void Update()
    {
        value = itemIn; //To debug the slot no longer getting values
    }
}
