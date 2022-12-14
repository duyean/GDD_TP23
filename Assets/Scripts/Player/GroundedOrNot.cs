using UnityEngine;

public class GroundedOrNot : MonoBehaviour {
    public bool grounded;
    private Vector3 direction;
    private float thickness = 0.3f;

    void Start() {
        direction = new Vector3(0, -1.5f, 0);
    }

    void FixedUpdate() {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, thickness, Vector3.down, 1.5f);
        if (hit) {
            print(hit.transform.name);
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground")) {
                grounded = true;
            }
        } else {
            grounded = false;
        }
    }
}