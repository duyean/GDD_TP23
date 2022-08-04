using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	public GameObject canvas;
	public string subject;
	public int level;
	public int questionNumber;
    private GameObject movementButtons;
    private MovementScript movementScript;
	
    // Start is called before the first frame update
    void Start()
	{
        movementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScript>();
        canvas.SetActive(false);
        movementButtons = GameObject.FindGameObjectWithTag("MovementButtons");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.transform.tag == "Player")
		{
			canvas.SetActive(true);
            movementScript.StopMoving();
            if (movementButtons != null)
            {
                movementButtons.GetComponentInChildren<Canvas>().enabled = false;
            }
        }
	}
}
