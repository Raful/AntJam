using UnityEngine;
using System.Collections;

public class BuildShroomButton : MonoBehaviour 
{
	[SerializeField]
	private AbstractShroom m_shroomPrefab;

	public AbstractShroom shroomPrefab { get { return m_shroomPrefab; } }

	public bool canBeBuilt
	{
		get 
		{
			if (m_timeSinceLastBuild > 0 && m_timeSinceLastBuild < m_shroomPrefab.buildCooldown)
				return false;

			//TODO Check cost

			return true;
		}
	}

	private float m_timeSinceLastBuild = -1;

	void Update()
	{
		if (m_timeSinceLastBuild > -0.0001f) 
		{
			m_timeSinceLastBuild += Time.deltaTime;
		}
	}

	/// <summary>
	/// Called by a button in the GUI.
	/// </summary>
	public void OnClick()
	{
		ShroomBuilder.SelectShroom (this);
	}
}
