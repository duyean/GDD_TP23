using UnityEngine;

public class MinigameChest : MonoBehaviour
{
    public GameObject check;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            check.GetComponent<Check>().win = true;
        }
    }
}
