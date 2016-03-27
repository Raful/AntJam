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

    void OnDisable()
    {
        m_PylonList.Remove(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S was pressed.");
			this.gameObject.GetComponent<EventPlayer> ().PlayEvent ();
        }
    }

    static public List<PylonShroom> m_PylonList
    {
        get;
        private set;
    }

    [SerializeField]
    private int m_Range;
}