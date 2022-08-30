using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    // this script takes in the player's transform updates

    // Update is called once per frame
    void Update()
    {
        // this gameobj will follow the player
        gameObject.transform.position = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
    }
}
