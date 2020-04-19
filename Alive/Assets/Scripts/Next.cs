using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Next : MonoBehaviour
{
    public GameObject cam;
    private GameController gameController;
    public Text text;
    private TextContent texCon;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(NextPage);
        texCon = text.GetComponent<TextContent>();
        gameController = cam.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextPage()
    {
        gameController.phaseNow += 1;
        gameController.phaseNow = Mathf.Clamp(gameController.phaseNow, 0 , gameController.phase);
        text.text = texCon.P[gameController.phaseNow];
    }
}
