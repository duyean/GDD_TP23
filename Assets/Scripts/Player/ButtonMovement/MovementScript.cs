using UnityEngine;

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

    // move character left
    public void MoveLeft()
    {
        // set animation to walk
        Anim.SetBool("Walking", true);
        // enable movement in update
        moveLeft = true;
        // rotate player to face left
        Vector3 scale = new Vector3(-1, 1, 1);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().localScale = scale;
    }

    public void MoveRight()
    {
        // set animation to walk
        Anim.SetBool("Walking", true);
        // enable movement in update
        moveRight = true;
        // rotate player to face right
        Vector3 scale = new Vector3(1, 1, 1);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().localScale = scale;
    }

    public void Jump()
    {
        // if player is grounded, apply upwards force + play jump sound
        if (gameObject.GetComponent<GroundedOrNot>().grounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
            SFX.Instance.JumpSound();
        }
    }

    // code for stopping movement
    // called when key up or button exit
    public void StopMoving()
    {
        moveLeft = false;
        moveRight = false;
        // stop movement in x axis
        rb.velocity = new Vector2(0, rb.velocity.y);
        // set anim to idle
        Anim.SetBool("Walking", false);
    }

    // Update is called once per frame
    private void Update()
    {
        // add velocity to the rigidbody
        if (moveLeft)
        {

            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }

        if (moveRight)
        {

            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }

        // arrow input for movement
        if (Input.GetKey(KeyCode.UpArrow))
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

        // stop movement when keyUp
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            StopMoving();
        }


    }
}
