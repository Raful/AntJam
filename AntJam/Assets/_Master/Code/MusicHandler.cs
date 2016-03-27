using System.Collections;
using UnityEngine;
using FMODUnity;

public class MusicHandler : MonoBehaviour
{
    void Update()
    {
        var ep = gameObject.GetComponent<EventPlayer>();
        switch (Input.inputString)
        {
            case "1":
                ep.PlayEvent();
                break;
            case "2":
                ep.StopEvent(true);
                break;
            case "3":
                ep.ChangeParameter("intensity", ep.GetParamValue("intensity") - 0.1f);
                break;
            case "4":
                ep.ChangeParameter("intensity", ep.GetParamValue("intensity") + 0.1f);
                break;
            case "5":
                ep.ChangeParameter("menu", ep.GetParamValue("menu") - 0.1f);
                break;
            case "6":
                ep.ChangeParameter("menu", ep.GetParamValue("menu") + 0.1f);
                break;
        }
    }
}