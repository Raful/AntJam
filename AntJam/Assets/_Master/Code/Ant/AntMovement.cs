using UnityEngine;
using System.Collections;

public class AntMovement : MonoBehaviour 
{
	[SerializeField]
	private float m_speed = 1;

	// Use this for initialization
	void Start () 
	{
        
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		transform.Translate (Vector3.left * m_speed * Time.deltaTime);
	}
}
