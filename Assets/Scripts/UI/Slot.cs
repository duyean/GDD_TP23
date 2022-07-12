using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{

    public string firstItem;
    public string secondItem;
    private bool firstItemCheck = false;
    private bool secondItemCheck = false;
    int firstValue;
    int secondValue;
    //private bool anchoredItem;
    public int correctValue = 3;
    public GameObject questionText;

    public void Awake()
    {
        questionText.SetActive(false);
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            //anchoredItem = true;
            //if(anchoredItem == false)
            //{
            //    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<DragDrop>().currentLocation;
            //}
            if (firstItemCheck == false && secondItemCheck == false)
            {
                firstValue = int.Parse(eventData.pointerDrag.GetComponent<DragDrop>().itemValue);
                firstItemCheck = true;
            }
            else if (secondItemCheck == false && firstItemCheck == true)
            {
                secondValue = int.Parse(eventData.pointerDrag.GetComponent<DragDrop>().itemValue);
                secondItemCheck = true;
            }
            else if (firstItemCheck == true && secondItemCheck == true)
            {
                Debug.Log("AAAAAAAAAAAAAAAAAA");
                //CheckCorrect();
                
            }
            if (firstValue + secondValue == correctValue)
            {
                questionText.SetActive(true);
                Debug.Log("correct");
            }
        }
    }
    public void Update()
    {
        Debug.Log(firstValue);
        Debug.Log(secondValue);
    }
    

    //public void CheckCorrect()
    //{
    //    int firstValue = int.Parse(firstItem);
    //    int secondValue = int.Parse(secondItem);
    //    if (firstValue + secondValue == correctValue)
    //    {
    //        questionText.SetActive(true);
    //    }
    //}
}
