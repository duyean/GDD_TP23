using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tutorial from https://www.youtube.com/watch?v=kkAjpQAM-jE

public class Tile : MonoBehaviour
{

    [SerializeField] private Color baseColour, offsetColour;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject highlight;

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

    private void OnMouseDown()
    {

    }
}
