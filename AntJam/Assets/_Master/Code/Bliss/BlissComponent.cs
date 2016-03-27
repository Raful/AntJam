using UnityEngine;
using System.Collections;

public class BlissComponent : MonoBehaviour
{
    [SerializeField]
    public Bliss m_bliss;

	public bool isConverted { get { return m_bliss.currentBliss >= m_bliss.maxBliss; } }
}
