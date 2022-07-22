using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            this.gameObject.SetActive(false);
            other.gameObject.GetComponent<Player>().TakeDamage(1);
        }
        if(other.tag == "wall")
        {
            this.gameObject.SetActive(false);
        }
    }
}
