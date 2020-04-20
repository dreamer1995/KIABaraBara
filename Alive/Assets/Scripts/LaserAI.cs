using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAI : MonoBehaviour
{
    private float T;
    private Material mt;
    public Texture2D tu;
    public Collider cd;
    private bool canHurt;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
        canHurt = false;
        mt = GetComponent<Renderer>().material;
        cd = GetComponent<Collider>();
        T = 0;
        StartCoroutine(Launch());
    }

    // Update is called once per frame
    void Update()
    {
        T += Time.fixedDeltaTime * 4;
        if (transform.localScale.x > 0.2f && canHurt)
        {
            cd.enabled = true;
        }
        else
        {
            cd.enabled = false;
        }
    }
    IEnumerator Launch()
    {
        Vector3 pos = transform.position;
        while (true)
        {
            transform.position = new Vector3(pos.x, pos.y, Mathf.Lerp(pos.z, pos.z - 0.5f, T));
            if (T > 1.0f)
            {
                break;
            }
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        T = 0;
        while (true)
        {
            transform.position = new Vector3(pos.x, pos.y, Mathf.Lerp(pos.z - 0.5f, pos.z, T));
            if (T > 1.0f)
            {
                break;
            }
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        T = 0;
        while (true)
        {
            transform.position = new Vector3(pos.x, pos.y, Mathf.Lerp(pos.z, pos.z - 0.5f, T));
            if (T > 1.0f)
            {
                break;
            }
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        T = 0;
        while (true)
        {
            transform.position = new Vector3(pos.x, pos.y, Mathf.Lerp(pos.z - 0.5f, pos.z, T));
            if (T > 1.0f)
            {
                break;
            }
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        StartCoroutine(LaunchMain());
    }
    IEnumerator LaunchMain()
    {
        mt.SetTexture("_MainTex", tu);
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y, 0);
        transform.localScale = Vector3.zero;
        canHurt = true;
        T = 0;
        while (true)
        {
            transform.localScale = new Vector3(Mathf.Lerp(0, 1, T / 4), 1, 1);
            if (T / 4 > 1.0f)
            {
                break;
            }
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        yield return new WaitForSeconds(0.5f);
        T = 0;
        while (true)
        {
            transform.localScale = new Vector3(Mathf.Lerp(1, 0, T / 4), 1, 1);
            if (T / 4 > 1.0f)
            {
                break;
            }
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Astro")
        {
            Destroy(other.gameObject);
        }
    }
}
