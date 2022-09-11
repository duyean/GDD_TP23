using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler
{
    public Canvas canvas;
    public RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public string itemValue;
    public TextMeshProUGUI valueText;
    public Vector2 originalPos;
    public Button checkButton;
    public GameObject questionParent;
    public AnswerCheck answerCheck;

    public void Start()
    {
        answerCheck = questionParent.GetComponent<AnswerCheck>();
        valueText = GetComponentInChildren<TextMeshProUGUI>();
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        checkButton = GameObject.FindGameObjectWithTag("AnswerButton").GetComponent<Button>();
        checkButton.onClick.AddListener(ExecuteCheck); //When button is clicked, calls the ExecuteCheck function
        originalPos = rectTransform.anchoredPosition;  //Gets the original vector positions of every item at the start
    }

    //Set the items back to original position if the answer is wrong
    void ExecuteCheck()
    {
        if (answerCheck.correct == false)
        {
            rectTransform.anchoredPosition = originalPos;
        }
    }

    //Detect when the mouse holds down on the item
    public void OnBeginDrag(PointerEventData eventData)
    {

        //Set item transparent when dragging
        canvasGroup.alpha = .4f;
        //Allows raycasts to go through the item to detect the slot
        canvasGroup.blocksRaycasts = false;

    }

    //Detect when the mouse is moving the item
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; //Sets the item's position to the eventData which is the mouse cursor when the item is being dragged
        eventData.pointerDrag.GetComponent<BoxCollider2D>().enabled = false;

    }

    //Detect when the mouse lets go of the item
    public void OnEndDrag(PointerEventData eventData)
    {
        //Set item to translucent when no longer dragging
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        Cursor.lockState = CursorLockMode.None; //Unlocks the cursor
    }

    //Detect when the mouse clicks on the item
    public void OnPointerDown(PointerEventData eventData)
    {
        Cursor.lockState = CursorLockMode.Confined; //Locks the cursor so that it cannot go out of the screen while dragging
    }
}
