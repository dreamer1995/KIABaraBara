using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroAI : MonoBehaviour
{
    public float speed;
    public float speedRange;
    private Material mt;
    public Texture2D[] tex;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        transform.localScale = new Vector3(Random.Range(0.05f, 0.15f), transform.localScale.y, Random.Range(0.05f, 0.15f));
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        speed = Random.Range(speed - speedRange, speed + speedRange);
        mt = GetComponent<Renderer>().material;
        int a = (int)Random.Range(0, 2.999f);
        mt.SetTexture("_MainTex", tex[a]);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, -speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MyBullet")
        {
            player.HP += 0.25f;
            player.FreshHP();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
