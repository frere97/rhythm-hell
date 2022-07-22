using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    BackgroudColorChange backgroudColorChange;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    public GameObject bullet;
    public List<GameObject> bullets = new List<GameObject>();
    public int NumberOfBullets = 60;
    public float speed = 6f;
    public static Player instance;
    public int MaxHealth = 10;
    public int CurrentHealth;
    bool canTakeDamage = true;
    public GameObject DeathMenu;
    bool CanShoot = true;
    bool CanDash = true;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
       
        
        rb = GetComponent<Rigidbody2D>();
        backgroudColorChange = BackgroudColorChange.instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
        CreateBullets();
        CurrentHealth = MaxHealth;

    }



    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Shoot();
        //set the color of the player the opposite of the background color
         Color backgroudColor = BackgroudColorChange.instance.spriteRenderer.color;
        spriteRenderer.color = new Color(1 - backgroudColor.r, 1 - backgroudColor.g, 1 - backgroudColor.b);
        LimiteMovement();
        Dash();
    }

    void Dash()
    {
        if (Input.GetKey(KeyCode.Space) && CanDash)
        {
            rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime)* 6;
            StartCoroutine(DashIE());
        }


    }

    IEnumerator DashIE(){
        CanDash = false;
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        yield return new WaitForSeconds(0.1f);
        collider.enabled = true;
        yield return new WaitForSeconds(2f);
        CanDash = true;

    }

    void LimiteMovement(){
        if(transform.position.x > backgroudColorChange.transform.position.x + backgroudColorChange.spriteRenderer.bounds.size.x / 2){
            transform.position = new Vector3(backgroudColorChange.transform.position.x + backgroudColorChange.spriteRenderer.bounds.size.x / 2, transform.position.y, transform.position.z);
        }
        if(transform.position.x < backgroudColorChange.transform.position.x - backgroudColorChange.spriteRenderer.bounds.size.x / 2){
            transform.position = new Vector3(backgroudColorChange.transform.position.x - backgroudColorChange.spriteRenderer.bounds.size.x / 2, transform.position.y, transform.position.z);
        }
        if(transform.position.y > backgroudColorChange.transform.position.y + backgroudColorChange.spriteRenderer.bounds.size.y / 2){
            transform.position = new Vector3(transform.position.x, backgroudColorChange.transform.position.y + backgroudColorChange.spriteRenderer.bounds.size.y / 2, transform.position.z);
        }
        if(transform.position.y < backgroudColorChange.transform.position.y - backgroudColorChange.spriteRenderer.bounds.size.y / 2){
            transform.position = new Vector3(transform.position.x, backgroudColorChange.transform.position.y - backgroudColorChange.spriteRenderer.bounds.size.y / 2, transform.position.z);
        }

    }

    void MovePlayer(){
        rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime);
        //look at the mouse
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
       
    }

        void CreateBullets()
    {

         GameObject bulletHolder = new GameObject("BulletHolder");
        for (int i = 0; i < NumberOfBullets; i++)
        {
            GameObject bullet = Instantiate(this.bullet, transform.position, Quaternion.identity, bulletHolder.transform);
            bullets.Add(bullet);
            bullet.SetActive(false);
        }
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            StartCoroutine(ShootIE());
        }
        
    }

    IEnumerator ShootIE()
    {

            if(SpectrumAnalise.instance.spectrumValue > 0.02f && CanShoot)
            {
            CanShoot = false;
            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].activeInHierarchy)
                {
                    bullets[i].transform.position = transform.position;
                    bullets[i].transform.rotation = transform.rotation;
                    bullets[i].SetActive(true);
                    break;
                }

            }
            yield return new WaitForSeconds(0.1f);
            CanShoot = true;
        }
    
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(TakeDamageCoroutine(damage));
         if (CurrentHealth <= 0)
        {
            DeathMenu.SetActive(true);
            Destroy(gameObject);
        }
        
    }

    IEnumerator TakeDamageCoroutine(int damage)
    {
        if (canTakeDamage)
        {
            canTakeDamage = false;
            CurrentHealth -= damage;
            yield return new WaitForSeconds(0.5f);
            canTakeDamage = true;
        }
       
    }
    
}
