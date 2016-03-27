using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class ButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointing at button.");
        transform.Find("TooltipBG").gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.Find("TooltipBG").gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start ()
    {
	
	}

	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed " + gameObject.ToString());
        }
	}
}
