using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject enemy;
    public float enemyInterval;
    public GameObject spawnArea1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            Vector3 minPoint = spawnArea1.transform.position - spawnArea1.transform.localScale / 2;
            Vector3 maxPoint = spawnArea1.transform.position + spawnArea1.transform.localScale / 2;
            Vector3 spawnPos = new Vector3 (Random.Range(minPoint.x, maxPoint.x),
                                            spawnArea1.transform.position.y,
                                            Random.Range(minPoint.z, maxPoint.z));
            Instantiate(enemy, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(enemyInterval);
        }
    }
}
