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
            rb.AddForce(Vector2.right);
        }
        if (Input.mousePosition.x < 0)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddForce(Vector2.left);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        if(collision != null)
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
        //Debug.Log(Input.mousePosition.x);
    }
}
