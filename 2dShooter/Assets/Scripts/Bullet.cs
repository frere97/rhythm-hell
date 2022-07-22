using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.up * speed;

         Color backgroudColor = BackgroudColorChange.instance.spriteRenderer.color;

         spriteRenderer.color = new Color(0.5f - backgroudColor.r, 0.5f  - backgroudColor.g, 0.5f - backgroudColor.b);
    }
}
