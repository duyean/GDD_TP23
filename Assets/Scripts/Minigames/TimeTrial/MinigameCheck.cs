using TMPro;
using UnityEngine;

public class MinigameCheck : MonoBehaviour
{

    public GameObject moveButton;

    public float timeLeft;


    bool isChecking;
    public bool win;

    private GameObject player;

    public TextMeshProUGUI timeLeftText;


    private void Start()
    {
        timeLeft = 60f;
        isChecking = false;
        player = GameObject.FindGameObjectWithTag("Player");
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
        if (!isChecking && !win && Vector3.Distance(transform.position, player.transform.position) <= 1f)
        {
            moveButton.SetActive(true);
            isChecking = true;
        }

        // checks for lose
        if (timeLeft <= 0 && isChecking)
        {
            moveButton.SetActive(false);
            WinUI.Instance.loseMinigameUI.SetActive(true);
            isChecking = false;
        }


        // checks for win
        if (timeLeft >= 0 && win && isChecking)
        {
            moveButton.SetActive(false);
            WinUI.Instance.winMinigameUI.SetActive(true);
            isChecking = false;

        }



        timeLeftText.text = "Time Left: " + timeLeft.ToString("F2");

    }



}
