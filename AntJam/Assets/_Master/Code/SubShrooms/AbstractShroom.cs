using UnityEngine;
using System.Collections;

/// <summary>
/// Abstract class for all sub shrooms.
/// </summary>
public abstract class AbstractShroom : MonoBehaviour
{
    [SerializeField]
    private int m_Cost;
    [SerializeField][Tooltip("Construction cooldown in ms")]
    private int m_BuildCooldown;

	public int cost { get { return m_Cost; } }
	public int buildCooldown { get { return m_BuildCooldown; } }
	public bool canBeBuilt 
	{ 
		get 
		{
			if (m_buildCooldownTimer >= 0 && m_buildCooldownTimer < m_BuildCooldown)
				return false;

			//TODO Check the cost

			return true;
		}
	}

	private float m_buildCooldownTimer = -1;

	void LateUpdate() //Should be Update(), but I'm too lazy to fix compatibility with subclasses
	{
		//Update cooldown timer
		if (m_buildCooldownTimer >= -0.0001f)
			m_buildCooldownTimer += Time.deltaTime;
	}
}
