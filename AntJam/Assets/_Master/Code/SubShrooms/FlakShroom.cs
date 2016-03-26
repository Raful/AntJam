using UnityEngine;
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
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_CooldownTimer -= Time.deltaTime;

        // Prioritize enemies closest to home base.
        if (m_CooldownTimer <= 0)
        {
            Ray ray = new Ray(transform.position + Vector3.up * 500 + Vector3.left * m_MaxRange, Vector3.right);
            foreach (RaycastHit hit in Physics.RaycastAll(ray, m_MaxRange - m_MinRange))
            {
                if (hit.collider.tag == "Enemy")
                {
                    hit.collider.GetComponent<HPComponent>().hp.DealDamage(m_Damage);
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
                    m_CooldownTimer = m_AttackCooldown;
                    break;
                }
            }
        }
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
