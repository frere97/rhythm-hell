using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Enemy>())
        {
            this.gameObject.SetActive(false);
            other.gameObject.GetComponent<Enemy>().LoseHealth(1);
        }
        if(other.tag == "wall")
        {
            this.gameObject.SetActive(false);
        }
    }
}
