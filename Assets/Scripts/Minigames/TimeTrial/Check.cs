using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Check : MonoBehaviour
{
    public GameObject LoseUI;


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
        // checks if time should be ticking
        if (isChecking)
        {
            timeLeft -= Time.deltaTime;
        }

        // sets time to tick and enables movement if camera is on player
        if (!isChecking && !win && Vector3.Distance(transform.position, player.transform.position)<=1f)
        {
            moveButton.SetActive(true);
            setMoveButton = false;
            isChecking = true;
        }

        // checks for lose
        if(timeLeft <= 0 && isChecking)
        {
            WinUI.Instance.loseUI.SetActive(true);
            isChecking = false;
        }


        // checks for win
        if (timeLeft >= 0 && win)
        {
            WinUI.Instance.winUI.SetActive(true) ;
            isChecking = false;
        }

        

        timeLeftText.text = "Time Left: " + timeLeft.ToString("F2");

    }



}
