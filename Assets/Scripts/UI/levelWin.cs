using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelWin : MonoBehaviour
{
    public GameObject treasure;
    public bool win = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (win == true)
        {
            Destroy(treasure);
            WinUI.Instance.winUI.SetActive(true);
        }
    }

    
}
