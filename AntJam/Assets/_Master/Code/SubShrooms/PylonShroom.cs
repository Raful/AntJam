using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;

/// <summary>
/// Shroom enabling placing additional shrooms in a set range.
/// </summary>
public class PylonShroom : AbstractShroom
{

	void OnEnable ()
    {
        // If null list, instantiate it
        if (m_PylonList == null)
        {
            m_PylonList = new List<PylonShroom>();
        }

        m_PylonList.Add(this);
    }

    void Start()
    {
        gameObject.GetComponent<EventPlayer>().PlayEvent();
        StartCoroutine(ExecuteAfterTime(4f));
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        if (!gameObject.GetComponent<EventPlayer>().UpdateEventToPlay("event:/Player/Hurt"))
            Debug.Log("Could not change sound event.");
    }

    void OnDisable()
    {
        m_PylonList.Remove(this);
    }

    static public List<PylonShroom> m_PylonList
    {
        get;
        private set;
    }

    [SerializeField]
    private int m_Range;
}