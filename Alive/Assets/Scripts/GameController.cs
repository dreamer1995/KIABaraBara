using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject enemy;
    public float enemyInterval;
    public GameObject spawnArea1;
    public float branch;
    public GameObject grid;
    public int[,] puzzleMap;
    public int[,] answerMap;
    public bool puzzleClear;
    public Text text;
    public Text text2;
    // Start is called before the first frame update
    void Start()
    {
        puzzleClear = false;
        puzzleMap = new int[5, 5] {
            { 0,0,0,0,0 },
            { 0,0,0,0,0 },
            { 0,0,0,0,0 },
            { 0,0,0,0,0 },
            { 0,0,0,0,0 }
        };
        answerMap = new int[5,5] {
            { 1,0,0,0,1 },
            { 0,0,0,0,0 },
            { 0,1,0,1,0 },
            { 1,0,0,0,1 },
            { 1,1,0,1,1 }
        };
        if (branch == 0) 
        {
            StartCoroutine(SpawnEnemy());
        }
        else
        {
            StartCoroutine(GenerateGrids(puzzleMap));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(puzzleClear)
        {
            text2.gameObject.SetActive(true);
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
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
    IEnumerator GenerateGrids(int[,] puzzleMap)
    {
        text.gameObject.SetActive(false);
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject _grid = Instantiate(grid, new Vector3(-27.0f + j, 0.5f, 2.0f - i), Quaternion.identity);
                _grid.name = i + "" + j;
                //if(puzzleMap[i,j] == 1)
                //    _grid.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
            }
        }
        yield return new WaitForSeconds(0);
    }
}
