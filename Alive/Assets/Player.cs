using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 pos;
    public float speed;
    public Vector4 boundaries;

    // Start is called before the first frame update
    void Start()
    {
        pos = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(speed, 0 , 0);
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
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, boundaries.x, boundaries.y),
            0 ,Mathf.Clamp(transform.position.z, boundaries.z, boundaries.w));
    }
}
