using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public float MaxHealth = 100f;
    [SerializeField]public float CurrentHealth;
    public float speed = 5f;
    protected Player player;
    
    void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    void Start()
    {
        
        player = Player.instance;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentHealth <= 0)
        {
            Destroy(gameObject);
            

        }
    }

    public void LoseHealth(float damage)
    {
        CurrentHealth -= damage;
    }
}
