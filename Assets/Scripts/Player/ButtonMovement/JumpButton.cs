using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScript>().Jump();
        SFX.Instance.JumpSound();
    }

}

