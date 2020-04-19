using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchCam : MonoBehaviour
{
    public GameObject sndCam;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(SwitchCamera);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SwitchCamera()
    {
        if (sndCam.activeSelf == false)
        {
            sndCam.SetActive(true);
        }
        else
        {
            sndCam.SetActive(false);
        }
    }

}
