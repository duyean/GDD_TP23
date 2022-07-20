using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public string value;

    public void OnDrop(PointerEventData eventData) 
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition; //Anchors the item to the slot when it is dropped on the slot
            value = eventData.pointerDrag.GetComponent<DragDrop>().itemValue; //Gets the itemValue from the object and parses into the value variable
        }
    }
}