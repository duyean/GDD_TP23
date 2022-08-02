using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlockUpright : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;

    private void OnMouseDrag()
    {
        moveBlock();
    }

    void moveBlock()
    {
        if(Input.mousePosition.y > 0)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.velocity = Vector3.up * speed;
        }
        if (Input.mousePosition.y < 0)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.velocity = Vector3.down * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != null && collision.transform.tag == "Wall")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
