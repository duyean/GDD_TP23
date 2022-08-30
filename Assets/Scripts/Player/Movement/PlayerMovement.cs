using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject[] pets;


    public CharacterController2D controller;
    public float runspeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;



    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runspeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }
    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
