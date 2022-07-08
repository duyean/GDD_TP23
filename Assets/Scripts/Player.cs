using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public class Player : MonoBehaviour
    {
        public StateMachine movementSM;
        public Grounded_State grounded;
        public Jumping_State jumping;

        public float gravity = 0.5f;
        public Rigidbody2D rb;
        public Vector2 direction;
        public float speed = 10;
        public float jumpForce = 5;
        public LayerMask groundLayer;

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision != null && collision.collider.gameObject.layer == 6 || collision.collider.gameObject.layer == 7)
            {
                jumping.grounded = true;
            }
            else
            {
                jumping.grounded = false;
            }
        }

        public void move()
        {
            direction = new Vector2(Input.GetAxis("Horizontal"), -gravity);
            rb.velocity = direction * speed;
        }
        public void jump()
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Start is called before the first frame update
        void Start()
        {
            rb = transform.GetComponent<Rigidbody2D>();
            movementSM = new StateMachine();
            grounded = new Grounded_State(this, movementSM);
            jumping = new Jumping_State(this, movementSM);
            movementSM.Initialize(grounded);
        }

        // Update is called once per frame
        void Update()
        {
            movementSM.CurrentState.HandleInput();
            movementSM.CurrentState.LogicUpdate();
        }

        void FixedUpdate()
        {
            movementSM.CurrentState.PhysicsUpdate();
        }
    }

}
