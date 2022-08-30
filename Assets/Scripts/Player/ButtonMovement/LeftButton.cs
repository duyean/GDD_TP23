using UnityEngine;
using UnityEngine.EventSystems;

public class LeftButton : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScript>().MoveLeft();
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
