using UnityEngine;
using System.Collections;

public class ShroomBuilder : MonoBehaviour 
{
	private static BuildShroomButton m_selectedShroomType;

	public static void SelectShroom(BuildShroomButton shroom)
	{
		Debug.Log ("Shroom selected");
		m_selectedShroomType = shroom;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			if (m_selectedShroomType != null) 
			{
				StartCoroutine (HandleMousePress ());
			}
		}
	}

	private IEnumerator HandleMousePress()
	{
		Debug.Log ("HandleMousePress");
		BuildShroomButton currentSelectedShroom = m_selectedShroomType;

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		Debug.DrawRay (ray.origin, ray.direction * 1000, Color.blue, 20f);
		Debug.Log (ray);

		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) 
		{
			if (hit.collider.tag == "MiddleCollider")
			{
				Debug.Log ("Collided");
				Vector3 mousePosition = hit.point;

				//Wait to see if a button was pressed
				yield return null;

				if (currentSelectedShroom == m_selectedShroomType) 
				{
					//Still on the same shroom; no button was pressed. Spawn a shroom
					Debug.Log ("Mouse position: " + mousePosition);
					GameObject newObject = Instantiate<GameObject> (m_selectedShroomType.shroomPrefab.gameObject);
					newObject.transform.localPosition = mousePosition;
				}
			}
		}
	}
}
