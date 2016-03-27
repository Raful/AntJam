﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Melee range shroom. Instantly fills enemy bliss bar and then self destructs.
/// </summary>
public class BlissShroom : AbstractShroom
{

	// Use this for initialization
	void Start ()
    {
        gameObject.GetComponent<EventPlayer>().PlayEvent();
        StartCoroutine(ExecuteAfterTime(4f));
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        if (!gameObject.GetComponent<EventPlayer>().UpdateEventToPlay("event:/Player/Hurt"))
            Debug.Log("Could not change sound event.");
    }

    // Update is called once per frame
    void Update ()
    {
        Ray ray = new Ray(transform.position, Vector3.right);
        foreach(RaycastHit hit in Physics.RaycastAll(ray, 0.5f)) // What is proper melee range?
        {
            if (hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<BlissComponent>().m_bliss.AddBliss(hit.collider.GetComponent<BlissComponent>().m_bliss.maxBliss);
                this.GetComponent<HPComponent>().hp.DealDamage(this.GetComponent<HPComponent>().hp.maxHp);
            }
        }
    }
}
