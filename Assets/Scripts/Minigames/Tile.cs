using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tutorial from https://www.youtube.com/watch?v=kkAjpQAM-jE

public class Tile : MonoBehaviour
{

    [SerializeField] private Color baseColour, offsetColour;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject highlight;
    [SerializeField] private Rigidbody2D rb;

    public void Init(bool isOffset)
    {
        spriteRenderer.color = isOffset ? offsetColour : baseColour;
    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    private void OnMouseDrag()
    {
        rb.MovePosition(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, transform.localPosition.z));
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision != null)
    //    {

    //    }
    //}
}
