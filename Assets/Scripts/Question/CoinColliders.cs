using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinColliders : MonoBehaviour
{

	public GameObject canvas;

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
		if(collision.transform.tag == "Coin")
        {
			Debug.Log("collision detected");
			canvas.SetActive(true);
		}
	}
}
