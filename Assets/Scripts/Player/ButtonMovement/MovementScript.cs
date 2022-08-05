using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementScript : MonoBehaviour
{
    private Rigidbody2D rb;

    private float moveSpeed, jumpForce;

    private bool moveLeft, moveRight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 5f;
        jumpForce = 500f;
        moveLeft = false;
        moveRight = false;
    }

    public void MoveLeft()
    {
        moveLeft = true;
        Vector3 scale = new Vector3(-1,1,1);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().localScale = scale;
    }

    public void MoveRight()
    {
        moveRight = true;
        Vector3 scale = new Vector3(1, 1, 1);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().localScale = scale;
    }

    public void Jump()
    {
        if (rb.velocity.y == 0)
        {
            rb.AddForce(Vector2.up * jumpForce);
            SFX.Instance.JumpSound();
        }
    }

    public void StopMoving()
    {
        moveLeft = false;
        moveRight = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    // Update is called once per frame
    private void Update()
    {
        if (moveLeft)
        {

            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y) ;
        }

        if (moveRight)
        {

            rb.velocity = new Vector2(moveSpeed, rb.velocity.y) ;
        }

    }
}
