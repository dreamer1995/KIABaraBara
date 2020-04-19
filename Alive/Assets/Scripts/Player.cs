using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject myBullet;
    public GameObject repairBar;
    private Material uimt;
    private Vector3 pos;
    public float speed;
    public Vector4 boundaries;
    private bool canShoot;
    private float intervalTime;
    public float bulletPerSecond;
    public GameObject cam;
    private GameController gameController;
    public int HP;
    private bool phaseFlag1;
    private bool phaseFlag2;
    private bool phaseFlag3;
    private bool phaseFlag4;
    private Material mt;
    public Texture2D[] flight;
    private float balance;
    public float balanceSpeed;
    private float time;
    public float invincible;
    // Start is called before the first frame update
    void Start()
    {
        balance = 2.5f;
        mt = GetComponent<Renderer>().material;
        uimt = repairBar.GetComponent<RawImage>().material;
        phaseFlag1 = true;
        phaseFlag2 = true;
        phaseFlag3 = true;
        phaseFlag4 = true;
        HP = 9;
        FreshHP();
        gameController = cam.GetComponent<GameController>();
        pos = GetComponent<Transform>().position;
        canShoot = true;
        intervalTime = 0.0f;
        StartCoroutine(AutoRepair());
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (!gameController.freeze)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.position -= new Vector3(speed, 0, 0);
                balance -= Time.deltaTime * balanceSpeed;
                balance = Mathf.Clamp(balance, 0, 4);
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += new Vector3(0, 0, speed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position -= new Vector3(0, 0, speed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(speed, 0, 0);
                balance += Time.deltaTime * balanceSpeed;
                balance = Mathf.Clamp(balance, 0, 4);
            }
            else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                if(balance < 2.5)
                balance += Time.deltaTime * balanceSpeed;
                else if (balance > 2.5)
                balance -= Time.deltaTime * balanceSpeed;
            }
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, boundaries.x, boundaries.y),
            transform.position.y, Mathf.Clamp(transform.position.z, boundaries.z, boundaries.w));

        if(Input.GetKey(KeyCode.J) && canShoot)
        {
            Instantiate(myBullet, transform.position + new Vector3(0, 0, 0.5f), Quaternion.identity);
            canShoot = false;
            intervalTime = 0.0f;
        }

        intervalTime += Time.deltaTime;
        if (intervalTime > 1 / bulletPerSecond)
        {
            canShoot = true;
        }

        if (HP > 24 && phaseFlag1)
        {
            gameController.phase = 1;
            gameController.complete = true;
            phaseFlag1 = false;
        }
        if (HP > 49 && phaseFlag2)
        {
            gameController.phase = 2;
            gameController.complete = true;
            phaseFlag2 = false;
        }
        if (HP > 74 && phaseFlag3)
        {
            gameController.phase = 3;
            gameController.complete = true;
            phaseFlag3 = false;
        }
        if (HP > 99 && phaseFlag4)
        {
            gameController.phase = 4;
            gameController.complete = true;
            phaseFlag4 = false;
        }

        mt.SetTexture("_MainTex", flight[(int)balance]);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Bullet" || other.tag == "Astro")
        {
            HP -= 1;
            FreshHP();
            Destroy(other.gameObject);
        }
        if (other.tag == "BlackHole")
        {
            //if (time > invincible)
            {
                time = 0;
                HP -= 1;
                FreshHP();
            }
        }
    }
    void ChangeHPColor(int HP)
    {
        if (HP > 99)
        {
            uimt.SetColor("_Color", Color.white);
        }
        else if (HP > 60)
        {
            uimt.SetColor("_Color", Color.green);
        }
        else if(HP > 30)
        {
            uimt.SetColor("_Color", Color.yellow);
        }
        else
        {
            uimt.SetColor("_Color", Color.red);
        }
    }

    IEnumerator AutoRepair()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            HP += 1;
            FreshHP();
        }
    }
    public void FreshHP()
    {
        HP = Mathf.Max(0, HP);
        uimt.SetFloat("_Energy", HP);
        ChangeHPColor(HP);
    }
}

