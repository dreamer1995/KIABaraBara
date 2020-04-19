using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject bullet;
    public float interval;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Shoot()
    {
        while(true)
        {
            yield return new WaitForSeconds(interval);
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MyBullet")
        {
            player.HP += 1;
            player.FreshHP();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
