using UnityEngine;

public class SohaMovement : MonoBehaviour {
    private Rigidbody2D rb;
    private float moveSpeed, jumpForce;
    public int moveBtn;
    public Animator Anim;
    private bool stopMov;

    public bool grounded;
    private Vector3 direction;
    private float thickness = 0.3f;
    public static SohaMovement instance;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 5f;
        jumpForce = 500f;

        direction = new Vector3(0, -1.5f, 0);
        if (instance != null) {
            Destroy(this.gameObject);
        }
        SohaMovement.instance = this;
    }

    
    public void movePlayer(float dir) {
        if (dir != 0) {
            stopMov = true;
            Anim.SetTrigger("Run"); //Set Walking Animation
            rb.velocity = new Vector2(dir * moveSpeed, rb.velocity.y); //Move Player
            //Flip Player
            if (dir > 0) {
                this.transform.localScale = new Vector3(1, 1, 1); 
            } else if (dir<0) {
                this.transform.localScale = new Vector3(-1, 1, 1);
            }
        } else {
            Anim.SetTrigger("Stop Run");
            if (stopMov) {
                stopMov = false;
                 rb.velocity = new Vector2(dir * moveSpeed, rb.velocity.y);
            }
        }
    }
    public void Jump() {
        // if player is grounded, apply upwards force + play jump sound
        if (grounded) {
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

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, thickness, Vector3.down, 1.5f);
        if (hit)
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                grounded = true;
                Anim.SetBool("Jump", false);
            }
        }
        else
        {
            grounded = false;
            Anim.SetBool("Jump", true);
        }
    }
}
