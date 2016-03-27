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
}
