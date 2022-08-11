using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Check : MonoBehaviour
{
    public GameObject LoseUI;
    public GameObject WinUI;

    public GameObject moveButton;

    public float timeLeft;


    bool isChecking;
    bool setMoveButton;
    public bool win;

    public GameObject player;

    public TextMeshProUGUI timeLeftText;


    private void Start()
    {
        timeLeft = 60f;
        isChecking = false;
        setMoveButton = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isChecking)
        {
            timeLeft -= Time.deltaTime;
        }

        if (Vector3.Distance(transform.position, player.transform.position)<=1f)
        {
            moveButton.SetActive(true);
            setMoveButton = false;
            isChecking = true;
        }

        if(timeLeft <= 0 && isChecking)
        {
            LoseUI.SetActive(true);
            isChecking = false;
        }

        if (timeLeft >= 0 && win)
        {
            LoseUI.SetActive(true);
            isChecking = false;
        }

        

        timeLeftText.text = "Time Left: " + timeLeft.ToString("F2");

    }
}
