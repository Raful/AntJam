using UnityEngine;
using System.Collections;

public class FlyingAntMovement : AntMovement
{

	// Use this for initialization
	void Start () 
	{
		transform.position += Vector3.down * (PlaceOnGround.GetDistanceFromGround(GetComponent<Collider>()) - 8);
	}

	protected override void Update()
	{
		//Do nothing (override AntMovement's Update)
	}
	
	// Update is called once per frame
	protected override void FixedUpdate () 
	{
		if (!isHalted)
		{
			Vector3 velocity = Vector3.left * m_speed * Time.deltaTime;

			transform.Translate (velocity, Space.Self);
		}
	}
}
