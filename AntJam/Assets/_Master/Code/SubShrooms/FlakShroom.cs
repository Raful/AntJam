﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Ranged shroom with a minimum range. Can attack air.
/// </summary>
public class FlakShroom : AbstractShroom
{

	// Use this for initialization
	void Start ()
    {
        m_CooldownTimer = 0;
        gameObject.GetComponent<EventPlayer>().PlayEvent();
        StartCoroutine(ExecuteAfterTime(4f));
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        Debug.Log("Attempting to change sound event.");
        if (!gameObject.GetComponent<EventPlayer>().UpdateEventToPlay("event:/Player/Hurt"))
            Debug.Log("Could not change sound event.");
    }

    // Update is called once per frame
    void Update ()
    {
        m_CooldownTimer -= Time.deltaTime;

        // Prioritize enemies closest to home base.
        if (m_CooldownTimer <= 0)
        {
            Ray ray = new Ray(transform.position + Vector3.up * 8 + Vector3.left * m_MaxRange, Vector3.right);
            foreach (RaycastHit hit in Physics.RaycastAll(ray, m_MaxRange - m_MinRange))
            {
                if (hit.collider.tag == "Enemy")
                {
                    hit.collider.GetComponent<HPComponent>().hp.DealDamage(m_Damage);
                    hit.collider.GetComponent<HPComponent>().isHurt = true;
                    m_CooldownTimer = m_AttackCooldown;
                    break;
                }
            }
        }

        if (m_CooldownTimer <= 0)
        {
            Ray ray = new Ray(transform.position + Vector3.up * 500 + Vector3.right * m_MinRange, Vector3.right);
            foreach (RaycastHit hit in Physics.RaycastAll(ray, m_MaxRange - m_MinRange))
            {
                if (hit.collider.tag == "Enemy")
                {
                    hit.collider.GetComponent<HPComponent>().hp.DealDamage(m_Damage);
                    hit.collider.GetComponent<HPComponent>().isHurt = true;
                    m_CooldownTimer = m_AttackCooldown;
                    break;
                }
            }
        }
        CheckDamage();
    }

    /// <summary>
    /// Checks if the unit has recieved damage. Plays the hurt sound event if true.
    /// </summary>
    void CheckDamage()
    {
        var hpc = gameObject.GetComponent<HPComponent>();
        if (hpc.isHurt && hpc.isAlive)
        {
            gameObject.GetComponent<EventPlayer>().PlayEvent();
            hpc.isHurt = false;
        }
    }

    void OnDestroy()
    {
        gameObject.GetComponent<EventPlayer>().ChangeParameter("isDead", 1f);
        gameObject.GetComponent<EventPlayer>().PlayEvent();
    }

    [SerializeField]
    private int m_MinRange;
    [SerializeField]
    private int m_MaxRange;
    [SerializeField]
    private int m_Damage;
    [SerializeField][Tooltip("Attack cooldown in ms.")]
    private int m_AttackCooldown;
    private float m_CooldownTimer;
}
