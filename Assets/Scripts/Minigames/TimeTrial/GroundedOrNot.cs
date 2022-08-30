using UnityEngine;

public class GroundedOrNot : MonoBehaviour
{
    public bool grounded;
    private void Start()
    {
        grounded = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            grounded = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            grounded = false;
        }
    }
}
