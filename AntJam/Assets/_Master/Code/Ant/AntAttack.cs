using UnityEngine;
using System.Collections;

public class AntAttack : MonoBehaviour
{
    [SerializeField][Tooltip("The max distance between the ant and a shroom")]
    private float m_range = 1;
    [SerializeField]
    private int m_damage = 1;
    [SerializeField]
    private float m_attackCooldown = 1;

    private float m_timeSinceLastAttack = -1;
    private AntMovement m_movementComponent;

    void Awake()
    {
        m_movementComponent = GetComponent<AntMovement>();
    }
    
	// Update is called once per frame
	void Update ()
    {
        m_movementComponent.isHalted = false;

        Ray ray = new Ray(transform.position + Vector3.left * m_range, Vector3.right);
        foreach (RaycastHit hit in Physics.RaycastAll(ray, m_range * 2))
        {
            if (hit.collider.tag == "Ally")
            {
                HPComponent hpComponent = hit.collider.GetComponent<HPComponent>();
                if (hpComponent.isAlive)
                {
                    if (m_timeSinceLastAttack < 0 || m_timeSinceLastAttack >= m_attackCooldown)
                    {
                        //Make an attack
                        hpComponent.hp.DealDamage(m_damage);
                    }

                    //Stop moving while attacking
                    m_movementComponent.isHalted = true;
                }
            }
        }

        if (m_timeSinceLastAttack > -0.0001f)
        {
            m_timeSinceLastAttack += Time.deltaTime;
        }
	}
}
