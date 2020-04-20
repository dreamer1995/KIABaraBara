using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject laser;
    public GameObject blackHole;
    public GameObject astro;
    public float enemyInterval;
    public float blackHoleInterval;
    public float astroInterval;
    public GameObject spawnArea1;
    public float branch;
    public GameObject grid;
    public int[,] puzzleMap;
    public int[,] answerMap;
    public Text text;
    public Text text2;
    public int phase;
    public int phaseNow;
    public GameObject dataPanel;
    public GameObject puzzlePanel;
    public bool freeze;
    public bool complete = false;
    public bool inPuzzle = false;
    public Text data1;
    public Text data2;
    public Text data3;
    public Text data4;
    public GameObject sndCam;
    public Texture2D[] background;
    public GameObject place;
    private Material placemt;
    public GameObject[] grids;
    private bool firstFlag;
    private bool stepComplete;


    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1280, 720, false, 60);
        Application.targetFrameRate = 60;
        placemt = place.GetComponent<Renderer>().material;
        stepComplete = false;
        firstFlag = true;
        freeze = false;
        phase = 0;
        phaseNow = 0;
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
        StartCoroutine(GameProcess());
        //if (branch == 0) 
        //{
        //    //Time.timeScale = 0;
        //    StartCoroutine(SpawnEnemy());
        //}
        //else
        //{
        //    //Time.timeScale = 0;
        //    StartCoroutine(GenerateGrids(puzzleMap));
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            if(dataPanel.activeSelf == false)
            {
                Time.timeScale = 0;
                freeze = true;
                dataPanel.SetActive(true);
            }
        }
    }

    IEnumerator GameProcess()
    {
        bool stepSwitch = false;
        string data;
        while(!stepComplete)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            if (!stepSwitch)
            {
                stepSwitch = true;
                data = "1";
                StartCoroutine(GameProcessStep(data1, data, 5, 1));
            }
        }

        stepSwitch = false;
        stepComplete = false;
        while (!stepComplete)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            if (!stepSwitch)
            {
                stepSwitch = true;
                data = "2";
                StartCoroutine(GameProcessStep(data2, data, 5, 2));
            }
        }

        stepSwitch = false;
        stepComplete = false;
        while (!stepComplete)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            if (!stepSwitch)
            {
                stepSwitch = true;
                data = "3";
                StartCoroutine(GameProcessStep(data3, data, 5, 3));
            }
        }

        stepSwitch = false;
        stepComplete = false;
        while (!stepComplete)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            if (!stepSwitch)
            {
                stepSwitch = true;
                data = "4";
                StartCoroutine(GameProcessStep(data4, data, 5, 0));
            }
        }     
    }

    IEnumerator GameProcessStep(Text datax, string data, int gridSize, int nextPlaceindex)
    {
        bool stepSwitch = false;
        complete = false;
        while (!complete)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            if (!stepSwitch)
            {
                stepSwitch = true;
                StartCoroutine(SpawnEnemy());
                StartCoroutine(SpawnBlackHole());
                StartCoroutine(SpawnAstros());
            }
        }

        text2.text = "发现加密资料，准备解密";
        text2.gameObject.SetActive(true);

        yield return new WaitForSeconds(3);
        text2.gameObject.SetActive(false);
        stepSwitch = false;
        complete = false;
        while (!complete)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            if (!stepSwitch)
            {
                stepSwitch = true;
                inPuzzle = true;
                StartCoroutine(GenerateGrids(puzzleMap, gridSize));
            }
        }
        puzzleMap = new int[5, 5] {
            { 0,0,0,0,0 },
            { 0,0,0,0,0 },
            { 0,0,0,0,0 },
            { 0,0,0,0,0 },
            { 0,0,0,0,0 }
        };
        text2.text = "臭鸡";
        text2.gameObject.SetActive(true);
        datax.text = data;
        yield return new WaitForSeconds(2);
        text2.gameObject.SetActive(false);
        puzzlePanel.gameObject.SetActive(false);
        sndCam.SetActive(false);
        inPuzzle = false;
        text.gameObject.SetActive(true);
        dataPanel.SetActive(true);
        Time.timeScale = 0;

        yield return new WaitForSeconds(Time.deltaTime);
        placemt.SetTexture("_MainTex", background[nextPlaceindex]);
        text2.text = "继续战斗";
        text2.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        text2.gameObject.SetActive(false);

        stepComplete = true;
    }

    IEnumerator SpawnEnemy()
    {
        while (!complete)
        {
            yield return new WaitForSeconds(enemyInterval);
            if (complete)
                break;
            Vector3 minPoint = spawnArea1.transform.position - spawnArea1.transform.localScale / 2;
            Vector3 maxPoint = spawnArea1.transform.position + spawnArea1.transform.localScale / 2;
            Vector3 spawnPos = new Vector3 (Random.Range(minPoint.x, maxPoint.x),
                                            spawnArea1.transform.position.y + 0.02f,
                                            0.5f);
            Instantiate(laser, spawnPos, Quaternion.identity);
        }
    }
    IEnumerator SpawnBlackHole()
    {
        while (!complete)
        {
            yield return new WaitForSeconds(blackHoleInterval);
            if (complete)
                break;
            Vector3 minPoint = spawnArea1.transform.position - spawnArea1.transform.localScale / 2 + Vector3.one;
            Vector3 maxPoint = spawnArea1.transform.position + spawnArea1.transform.localScale / 2 - Vector3.one;
            Vector3 spawnPos = new Vector3(Random.Range(minPoint.x, maxPoint.x),
                                            spawnArea1.transform.position.y - 0.01f,
                                            Random.Range(minPoint.z, maxPoint.z));
            GameObject _blackHole = Instantiate(blackHole, spawnPos, Quaternion.Euler(0, Random.Range(0, 360), 0));
            float x = Random.Range(0.1f, 0.5f);
            _blackHole.transform.localScale = new Vector3(x, x, x);
        }
    }
    IEnumerator SpawnAstros()
    {
        while (!complete)
        {
            yield return new WaitForSeconds(astroInterval);
            if (complete)
                break;
            Vector3 minPoint = spawnArea1.transform.position - spawnArea1.transform.localScale / 2;
            Vector3 maxPoint = spawnArea1.transform.position + spawnArea1.transform.localScale / 2;
            Vector3 spawnPos = new Vector3(Random.Range(minPoint.x, maxPoint.x),
                                            spawnArea1.transform.position.y,
                                            Random.Range(minPoint.z, maxPoint.z));
            Instantiate(astro, spawnPos, Quaternion.identity);
        }
    }
    IEnumerator GenerateGrids(int[,] puzzleMap, int length)
    {
        if (!firstFlag)
        {
            for (int i = 0; i < length * length; i++)
            {
                Destroy(grids[i].gameObject);
            }
        }
        else
            firstFlag = false;
        freeze = true;
        text.gameObject.SetActive(false);
        puzzlePanel.gameObject.SetActive(true);
        sndCam.SetActive(true);
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                GameObject _grid = Instantiate(grid, new Vector3(-27.0f + j, 0.5f, 2.0f - i), Quaternion.identity);
                _grid.name = i + "" + j;
                //if(puzzleMap[i,j] == 1)
                //    _grid.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                grids[i * length + j] = _grid;
            }
        }
        yield return new WaitForSeconds(0);
        Time.timeScale = 0;
    }
}
