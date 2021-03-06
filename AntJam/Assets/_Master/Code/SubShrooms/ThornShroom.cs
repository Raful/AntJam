﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Shroom causing AoE damage. Cannot attack air.
/// </summary>
public class ThornShroom : AbstractShroom
{

	// Use this for initialization
	void Start ()
    {
        m_CooldownTimer = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_CooldownTimer =- Time.deltaTime * 1000;

        if (m_CooldownTimer <= 0)
        {
            // Since ants can stack a raycast must be done on the entire antstack
            for (int height = 0; height < 8; height++)
            {
                Ray ray = new Ray(transform.position + Vector3.up * height + Vector3.left * m_Range, Vector3.right);
                foreach (RaycastHit hit in Physics.RaycastAll(ray, m_Range * 2))
                {
                    if (hit.collider.tag == "Enemy")
                    {
                        hit.collider.GetComponent<HPComponent>().hp.DealDamage(m_Damage);
                        Debug.Log("Dealt " + m_Damage.ToString() + " damage.");
                    }
                }
            }

            // Reset cooldown
            m_CooldownTimer = m_AttackCooldown;
        }
    }

    [SerializeField]
    private int m_Damage;
    [SerializeField]
    private int m_AttackCooldown;
    [SerializeField]
    private int m_Range;
    private float m_CooldownTimer;
}
