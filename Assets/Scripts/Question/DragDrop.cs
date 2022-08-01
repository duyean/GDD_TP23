using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Canvas canvas; 
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public string itemValue;
    public TextMeshProUGUI valueText;

    public void Start()
    {
        valueText = GetComponentInChildren<TextMeshProUGUI>();
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        valueText.SetText(itemValue);
    }

    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .4f; //Set item transparent when dragging
        canvasGroup.blocksRaycasts = false; //Allows raycasts to go through the item to detect the slot
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; //Sets the item's position to the eventData which is the mouse cursor when the item is being dragged
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f; //Set item to translucent when no longer dragging
        canvasGroup.blocksRaycasts = true; //Allow item to block raycasts.
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

}
