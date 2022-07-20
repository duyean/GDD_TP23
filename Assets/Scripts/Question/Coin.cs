using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

	public GameObject canvas;
	public int level;
	public int questionNumber;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log(collision.transform.tag);
		if(collision.transform.tag == "Player")
        {
			canvas.SetActive(true);
		}
	}
}
