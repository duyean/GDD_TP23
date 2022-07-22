using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

	public GameObject canvas;
	public int level;
	public int questionNumber;
	public GameObject player;
	private Rigidbody2D rb2d;
	// Start is called before the first frame update
	void Start()
	{
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
			//Time.timeScale = 0;
			Freeze();
		}
        
	}
	void Freeze()
	{
		rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
	}
}
