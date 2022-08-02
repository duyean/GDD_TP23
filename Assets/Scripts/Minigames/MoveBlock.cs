using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;

    private void OnMouseDrag()
    {
        moveBlock();
    }

    void moveBlock()
    {
        if(Input.mousePosition.x > 0)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = Vector3.right * speed;
        }
        if (Input.mousePosition.x < 0)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = Vector3.left * speed;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if(rb.velocity == Vector2.zero)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
