using UnityEngine;
using System.Collections;

public class GoopShroom : AbstractShroom
{
    public int Range { get { return m_range; } }
    public float SlowForce { get { return m_slow; } }
	// Use this for initialization
	void OnEnable ()
    {
        if (m_slow > 100)
            m_slow = 100;
        if (m_slow < 0)
            m_slow = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    //TODO Figure out how to apply slow.

	}

    [SerializeField]
    private int m_range;
    [SerializeField][Tooltip("Slow percentage, given in 1-100.")]
    private float m_slow;
}
