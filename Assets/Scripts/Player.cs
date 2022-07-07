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

        public Rigidbody2D rb;
        public Vector2 direction;
        public float speed = 10;
        public float jumpForce = 5;
        public LayerMask groundLayer;

        public bool CheckGrounded(Vector2 position)
        {
            StartCoroutine(checkGrounded(position));
        }

        public IEnumerator checkGrounded(Vector2 position)
        {
            yield return new WaitForSeconds(2);
            Collider2D hit = Physics2D.OverlapCircle(position, 1, groundLayer);
            if(hit != null)
            {
                yield return true;
            }
            else
            {
                yield return false;
            }
        }

        public void move()
        {
            direction = new Vector2(Input.GetAxis("Horizontal"), 0);
            rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
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
