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

        Debug.Log("Attempting to change sound event.");
        if (!gameObject.GetComponent<EventPlayer>().UpdateEventToPlay("event:/Player/Hurt"))
            Debug.Log("Could not change sound event.");
    }

    void Update()
    {
        var hpc = gameObject.GetComponent<HPComponent>();
        if(hpc.isHurt && hpc.isAlive)
        {
            gameObject.GetComponent<EventPlayer>().PlayEvent();
            hpc.isHurt = false;
        }
    }

    void OnDisable()
    {
        m_PylonList.Remove(this);
    }

    void OnDestroy()
    {
        gameObject.GetComponent<EventPlayer>().ChangeParameter("isDead", 1f);
        gameObject.GetComponent<EventPlayer>().PlayEvent();
    }

    static public List<PylonShroom> m_PylonList
    {
        get;
        private set;
    }

    [SerializeField]
    private int m_Range;
}