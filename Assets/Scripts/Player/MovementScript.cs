using UnityEngine;

public class MovementScript : MonoBehaviour {
    private Rigidbody2D rb;
    private float moveSpeed, jumpForce;
    public int moveBtn;
    public Animator Anim;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 5f;
        jumpForce = 500f;
    }


    public void movePlayer(float dir) {
        if (dir != 0) {
            Anim.SetBool("Walking", true); //Set Walking Animation
            rb.velocity = new Vector2(dir * moveSpeed, rb.velocity.y); //Move Player
            //Flip Player
            if (dir > 0) {
                GetComponent<SpriteRenderer>().flipX = false;
            } else {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        } else {
            Anim.SetBool("Walking", false);
        }
    }
    public void Jump() {
        // if player is grounded, apply upwards force + play jump sound
        if (gameObject.GetComponent<GroundedOrNot>().grounded) {
            rb.AddForce(Vector2.up * jumpForce);
            SFX.Instance.JumpSound();
        }
    }

    // Update is called once per frame
    private void Update() {
        //Move Player
        movePlayer(Mathf.Clamp(Input.GetAxis("Horizontal") + moveBtn, -1, 1));

        //Jump Control
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }
    }
}
