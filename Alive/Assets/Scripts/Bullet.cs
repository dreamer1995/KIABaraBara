using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject player;
    public float speed;
    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        transform.LookAt(player.transform);
        Vector3 playerPos = player.GetComponent<Transform>().position;
        dir = (playerPos - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * speed;
    }
}
