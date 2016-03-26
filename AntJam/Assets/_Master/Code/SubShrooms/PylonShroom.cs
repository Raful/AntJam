using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Shroom enabling placing additional shrooms in a set range.
/// </summary>
public class PylonShroom : AbstractShroom
{

	// Use this for initialization
	void OnEnable ()
    {
        m_PylonList.Add(this);
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