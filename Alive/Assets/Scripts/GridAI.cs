using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridAI : MonoBehaviour
{
    private GameObject cam;
    private GameController gameController;
    private Material mt;
    private int i;
    private int j;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        int num;
        int.TryParse(name, out num);
        i = num / 10;
        j = num % 10;
        mt = GetComponent<Renderer>().material;
        gameController = cam.GetComponent<GameController>();
        if (gameController.puzzleMap[i,j] == 1)
        {
            mt.SetColor("_Color", Color.blue);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if(gameController.puzzleMap[i, j] == 0)
        {
            GetComponent<Renderer>().material.SetColor("_Color", new Color(0, 0.627451f, 0.2509804f, 1));
            gameController.puzzleMap[i, j] = 1;
        }
        else
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            gameController.puzzleMap[i, j] = 0;
        }
        if(gameController.puzzleMap[i, j] == gameController.answerMap[i, j])
        {
            bool equal = true;
            for (int k = 0; k < 5; k++)
            {
                for (int l = 0; l < 5; l++)
                {
                    if (gameController.puzzleMap[k, l] != gameController.answerMap[k, l])
                    {
                        equal = false;
                        break;
                    }
                }
                if(!equal)
                {
                    break;
                }
            }
            if(equal)
            {
                gameController.complete = true;
                Time.timeScale = 1;
            }
        }

    }
}
