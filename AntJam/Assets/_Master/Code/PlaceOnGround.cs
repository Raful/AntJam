using UnityEngine;
using System.Collections;

public class PlaceOnGround : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        //Place the ant on the ground
        Ray ray = new Ray(transform.position + Vector3.up * 1000f, Vector3.down);
        foreach (RaycastHit hit in Physics.RaycastAll(ray, 2000f))
        {
            if (hit.collider.tag == "Ground") //Assumes all ground objects use the tag Ground
            {
                transform.position = hit.point + Vector3.up * GetComponent<Collider>().bounds.size.y / 2;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
