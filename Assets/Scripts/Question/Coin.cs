using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

	public GameObject canvas;
	public string subject;
	public int level;
	public int questionNumber;
	
    // Start is called before the first frame update
    void Start()
	{
        //canvas.SetActive(false);       
    }

	// Update is called once per frame
	void Update()
	{

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.transform.tag == "Player")
		{
			canvas.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScript>().StopMoving();
            if (GameObject.FindGameObjectWithTag("MovementButtons") != null)
            {
                GameObject.FindGameObjectWithTag("MovementButtons").SetActive(false);
            }

        }
        
	}
}
