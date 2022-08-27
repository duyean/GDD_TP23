using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementScript : MonoBehaviour
{
    private Rigidbody2D rb;

    private float moveSpeed, jumpForce;

    private bool moveLeft, moveRight;

    public Animator Anim;

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
        Anim.SetBool("Walking", true);
        moveLeft = true;
        Vector3 scale = new Vector3(-1,1,1);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().localScale = scale;
    }

    public void MoveRight()
    {
        Anim.SetBool("Walking", true);
        moveRight = true;
        Vector3 scale = new Vector3(1, 1, 1);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().localScale = scale;
    }

    public void Jump()
    {
        if (gameObject.GetComponent<GroundedOrNot>().grounded)
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
        Anim.SetBool("Walking", false) ;
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

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            StopMoving();
        }

        
    }
}
