using UnityEngine;
using System.Collections;

public class PlaceOnGround : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        //Move the object so that the collider touches the ground
        transform.position += Vector3.down * GetDistanceFromGround(GetComponent<Collider>());
    }
	
    /// <summary>
    /// Returns the distance between the bottom of the collider and the ground
    /// </summary>
    /// <param name="collider"></param>
    /// <returns></returns>
    public static float GetDistanceFromGround(Collider collider)
    {
        Vector3 position = collider.bounds.center;
        Ray ray = new Ray(position + Vector3.up * 1000f, Vector3.down);
        foreach (RaycastHit hit in Physics.RaycastAll(ray, 2000f))
        {
            if (hit.collider.tag == "Ground") //Assumes all ground objects use the tag Ground
            {
                float yOffset = collider.bounds.size.y / 2 - (collider.bounds.center.y - position.y);
                return (position.y - yOffset) - hit.point.y;
            }
        }

        //The ray didn't hit the ground
        throw new System.Exception("There's no ground below the position " + position);
    }
}
