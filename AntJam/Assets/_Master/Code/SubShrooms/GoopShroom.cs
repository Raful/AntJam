using UnityEngine;
using System.Collections;

/// <summary>
/// Shroom applying a slow aura to nearby units with the enemy tag. Requires the enemies to have the RigidBody component.
/// The slow is handled in the child script GoopAura.cs
/// </summary>
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

    void start()
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

    [SerializeField]
    private int m_range;
    [SerializeField][Tooltip("Slow percentage, given in 1-100.")]
    private float m_slow;
}
