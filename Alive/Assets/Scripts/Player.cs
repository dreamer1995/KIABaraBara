using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject myBullet;
    public Text text;
    private Vector3 pos;
    public float speed;
    public Vector4 boundaries;
    public bool freeze;
    private bool canShoot;
    private float intervalTime;
    public float bulletPerSecond;

    // Start is called before the first frame update
    void Start()
    {
        pos = GetComponent<Transform>().position;
        canShoot = true;
        intervalTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!freeze)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.position -= new Vector3(speed, 0, 0);
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
            }
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, boundaries.x, boundaries.y),
            transform.position.y, Mathf.Clamp(transform.position.z, boundaries.z, boundaries.w));

        if(Input.GetKey(KeyCode.J) && canShoot)
        {
            Instantiate(myBullet, transform.position, Quaternion.identity);
            canShoot = false;
            intervalTime = 0.0f;
        }

        intervalTime += Time.deltaTime;
        if (intervalTime > 1 / bulletPerSecond)
        {
            canShoot = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            int HP = 0;
            int.TryParse(text.text, out HP);
            HP -= 1;
            text.text = HP.ToString();
            ChangeHPColor(HP);
        }
        if (other.tag == "Bullet")
        {
            int HP = 0;
            int.TryParse(text.text, out HP);
            HP -= 1;
            text.text = HP.ToString();
            ChangeHPColor(HP);
            Destroy(other.gameObject);
        }
    }
    void ChangeHPColor(int HP)
    {
        if (HP > 60)
        {
            text.GetComponent<Text>().color = Color.green;
        }
        else if(HP > 30)
        {
            text.GetComponent<Text>().color = Color.yellow;
        }
        else
        {
            text.GetComponent<Text>().color = Color.red;
        }
    }
}

