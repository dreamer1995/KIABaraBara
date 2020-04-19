using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextContent : MonoBehaviour
{
    public string[] P;
    public string defaultText;

    public Button data1;
    public Button data2;
    public Button data3;
    public Button data4;
    public Button data5;
    public GameObject cam;
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        defaultText = "Our current preoccupation with zombies and vampires is easy to explain. They're two sides of the same coin, addressing our fascination with sex, death and food. They're both undead, they both feed on us, they both pass on some kind of plague and they can both be killed with specialist techniques – a stake through the heart or a disembraining. But they seem to have become polarised. Vampires are the undead of choice for girls, and zombies for boys. Vampires are cool, aloof, beautiful, brooding creatures of the night. Typical moody teenage boys, basically. Zombies are dumb, brutal, ugly and mindlessly violent. Which makes them also like typical teenage boys, I suppose.";
        gameController = cam.GetComponent<GameController>();
        P = new string[4]
        {
            "1",
            "2",
            "3",
            "4"
        };

        data1.onClick.AddListener(ToData1);
        data2.onClick.AddListener(ToData2);
        data3.onClick.AddListener(ToData3);
        data4.onClick.AddListener(ToData4);
        data5.onClick.AddListener(ToData5);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameController.inPuzzle == false)
            {
                Time.timeScale = 1;
                gameController.freeze = false;
            }
            GetComponent<Text>().text = defaultText;
            transform.parent.gameObject.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            gameController.phaseNow -= 1;
            gameController.phaseNow = Mathf.Clamp(gameController.phaseNow, 0, 3);
            GetComponent<Text>().text = P[gameController.phaseNow];
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            gameController.phaseNow += 1;
            gameController.phaseNow = Mathf.Clamp(gameController.phaseNow, 0, 3);
            GetComponent<Text>().text = P[gameController.phaseNow];
        }
    }
    public void ToData1()
    {
        gameController.phaseNow = 0;
        if(gameController.phase > 0)
        {
            GetComponent<Text>().text = P[0];
        }
        else
        {
            GetComponent<Text>().text = "???????";
        }
    }
    public void ToData2()
    {
        gameController.phaseNow = 1;
        if (gameController.phase > 1)
        {
            GetComponent<Text>().text = P[1];
        }
        else
        {
            GetComponent<Text>().text = "???????";
        }
    }
    public void ToData3()
    {
        gameController.phaseNow = 2;
        if (gameController.phase > 2)
        {
            GetComponent<Text>().text = P[2];
        }
        else
        {
            GetComponent<Text>().text = "???????";
        }
    }
    public void ToData4()
    {
        if (gameController.phase > 3)
        {
            GetComponent<Text>().text = P[3];
        }
        else
        {
            GetComponent<Text>().text = "???????";
        }
    }
    public void ToData5()
    {
        if (gameController.phase > 3)
        {
            GetComponent<Text>().text = P[3];
        }
        else
        {
            GetComponent<Text>().text = "???????";
        }
    }
}
