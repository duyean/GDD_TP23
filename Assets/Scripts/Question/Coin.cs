using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

	public GameObject canvas;
	public string subject;
	public int level;
	public int questionNumber;

    public GameObject player;
    public Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
	{
        player = GameObject.FindGameObjectWithTag("Player");
        canvas.SetActive(false);
        rb2d = player.GetComponent<Rigidbody2D>();
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
            GameObject.FindGameObjectWithTag("MovementButtons").SetActive(false);
        }
        
	}
}
