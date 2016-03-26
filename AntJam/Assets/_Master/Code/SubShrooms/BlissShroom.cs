using UnityEngine;
using System.Collections;

/// <summary>
/// Melee range shroom. Instantly fills enemy bliss bar and then self destructs.
/// </summary>
public class BlissShroom : AbstractShroom
{

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Ray ray = new Ray(transform.position, Vector3.right);
        foreach(RaycastHit hit in Physics.RaycastAll(ray, 2)) // What is proper melee range?
        {
            if (hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<BlissComponent>().m_bliss.AddBliss(hit.collider.GetComponent<BlissComponent>().m_bliss.maxBliss);
                this.GetComponent<HPComponent>().hp.DealDamage(this.GetComponent<HPComponent>().hp.maxHp);
            }
        }
	}

}
