using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

	public GameObject canvas;
	public string subject;
	public int level;
	public int questionNumber;

	//public GameObject player;
	//private Rigidbody2D rb2d;
	// Start is called before the first frame update
	void Start()
	{
		canvas.SetActive(false);
		//rb2d = player.GetComponent<Rigidbody2D>();
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
			Freeze();
		}
        
	}
    void Freeze()
    {
        //Disable joypad
    }
}
