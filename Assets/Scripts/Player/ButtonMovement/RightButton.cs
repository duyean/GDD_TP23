using UnityEngine;
using UnityEngine.EventSystems;

public class RightButton : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScript>().MoveRight();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScript>().StopMoving();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScript>().StopMoving();
    }
}
