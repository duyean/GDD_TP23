using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomiser : MonoBehaviour
{

    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0,sprites.Length)];
    }
}
