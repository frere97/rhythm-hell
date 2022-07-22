using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Enemy
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ChangeColor();
        
        Vector3 playerPos = Player.instance.transform.position;
        Vector3 direction = playerPos - transform.position;
        rb.velocity = direction * speed * Time.deltaTime;
        
    }

    void ChangeColor()
    {
        Color backgroudColor = BackgroudColorChange.instance.spriteRenderer.color;
        spriteRenderer.color = new Color( 1 - backgroudColor.r,  1 - backgroudColor.g,  1 - backgroudColor.b);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            this.gameObject.SetActive(false);
            other.gameObject.GetComponent<Player>().TakeDamage(1);
        }
    }
}
