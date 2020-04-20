using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonReact : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject arrow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        arrow.gameObject.SetActive(true);
        arrow.transform.position = new Vector3(775, transform.position.y, transform.position.z);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        arrow.gameObject.SetActive(false);
    }
}
