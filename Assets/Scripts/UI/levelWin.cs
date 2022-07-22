using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelWin : MonoBehaviour
{
    public GameObject treasure;
    public bool win = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (win == true)
        {
            WinUI.Instance.winUI.SetActive(true);
        }
    }

    
}
