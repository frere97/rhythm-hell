using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossOne : Enemy{
    public float RotationSpeed = 60f;
    Rigidbody2D rb;
    public int LifeToStage2 = 50;
    public int LifeToStage3 = 25;
    public GameObject bullet;
    public List<GameObject> bullets = new List<GameObject>();
    public int NumberOfBullets = 150;
    public GameObject[] objectsStage2;
    public GameObject[] objectsStage3;
    public List<GameObject> bulletSpawners = new List<GameObject>();
    SpriteRenderer spriteRenderer;
    bool orientationChanged = false;
     int orientation = 1;
     public GameObject Minion;
     bool CanInstantiateMinion = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CreateBullets();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Shoot());
        
    }

    void CreateBullets()
    {
        for (int i = 0; i < NumberOfBullets; i++)
        {
            GameObject bullet = Instantiate(this.bullet, transform.position, Quaternion.identity);
            bullets.Add(bullet);
            bullet.SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ChangeStage();
        //Movement();
        ChangeColor();
        Rotate();
        InstantiateMinions();

    }

    void ChangeColor()
    {
        Color backgroudColor = BackgroudColorChange.instance.spriteRenderer.color;
        spriteRenderer.color = new Color( 1 - backgroudColor.r,  1 - backgroudColor.g,  1 - backgroudColor.b, 0.2f);
    }

    void Movement()
    {
        //move to the player
        Vector3 playerPos = Player.instance.transform.position;
        Vector3 direction = playerPos - transform.position;
        rb.velocity = direction * speed;
        
    
    }

    void Rotate(){
        float NewRotationSpeed = RotationSpeed *(SpectrumAnalise.instance.filteredSpectrumValue * 20);
        if(SpectrumAnalise.instance.spectrumValue > 0.04f && !orientationChanged)
        {
            StartCoroutine(ChangeOrientation());
        }
        transform.Rotate(Vector3.forward, (NewRotationSpeed *orientation) * Time.deltaTime);

    }

    IEnumerator ChangeOrientation()
    {
        if(orientationChanged == false)
        {
        orientation *= -1;
        orientationChanged = true;
        }
        yield return new WaitForSeconds(2f);
        orientationChanged = false;
    }

    IEnumerator Shoot(){
        yield return new WaitForEndOfFrame();
        if(SpectrumAnalise.instance.spectrumValue > 0.03f)
        {        
        InstantiateBullets();
        
        }
        StartCoroutine(Shoot());


    }

    void InstantiateBullets()
    {
        foreach (GameObject bulletSpawner in bulletSpawners)
        {
            if (bulletSpawner.activeInHierarchy)
            {
                foreach (GameObject bullet in bullets)
                {
                    if (!bullet.activeInHierarchy)
                    {
                        bullet.transform.position = bulletSpawner.transform.position;
                        bullet.transform.rotation = bulletSpawner.transform.rotation;
                        bullet.SetActive(true);
                        break;
                    }
                }
            }
        }
    
    }

    void InstantiateMinions(){
            StartCoroutine(InstantiateMinionIE());
        
    }

    IEnumerator InstantiateMinionIE()
    {
        if(CanInstantiateMinion)
        {
            if(SpectrumAnalise.instance.spectrumValue > 0.04f)
            {
                    
            foreach (GameObject bulletSpawner in bulletSpawners)
            {
                if (bulletSpawner.activeInHierarchy)
                {
                    Instantiate(Minion, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
                }
            }
            CanInstantiateMinion = false;
            yield return new WaitForSeconds(5f);
            CanInstantiateMinion = true;
            }
        }

        
    }

    void ChangeStage(){
        if(CurrentHealth <= LifeToStage2)
        {
            foreach (GameObject obj in objectsStage2)
            {
                
                obj.SetActive(true);
            }
        }
        if(CurrentHealth <= LifeToStage3)
        {
            foreach (GameObject obj in objectsStage3)
            {
                obj.SetActive(true);
            }
        }
        
    }

    void OnDestroy()
    {
        SceneManager.LoadScene("Menu");
    }
}
