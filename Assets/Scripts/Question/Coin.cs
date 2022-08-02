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
        canvas.SetActive(false);       
    }

	// Update is called once per frame
	void Update()
	{

	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.transform.tag == "Player")
		{
			canvas.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScript>().StopMoving();
            if (GameObject.FindGameObjectWithTag("MovementButtons").activeSelf)
            {
                GameObject.FindGameObjectWithTag("MovementButtons").SetActive(false);
            }

        }
        
	}
}
