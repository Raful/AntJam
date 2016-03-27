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

		HPComponent hpComponent;
		if (GetTargetInRange (Vector3.left, out hpComponent) || GetTargetInRange(Vector3.right, out hpComponent)) 
		{
			if (m_timeSinceLastAttack < -0.0001f || m_timeSinceLastAttack >= m_attackCooldown)
			{
				//Make an attack
				hpComponent.hp.DealDamage(m_damage);
                hpComponent.isHurt = true;
				m_timeSinceLastAttack = 0;
			}

			//Stop moving while attacking
			m_movementComponent.isHalted = true;
		}

        if (m_timeSinceLastAttack > -0.0001f)
        {
            m_timeSinceLastAttack += Time.deltaTime;
        }
	}

	/// <summary>
	/// Gets the target in range.
	/// </summary>
	/// <returns><c>true</c>, if target in range was gotten, <c>false</c> otherwise.</returns>
	/// <param name="direction">Direction from the ant to check for a target.</param>
	/// <param name="outHPComponent">HP component of the target.</param>
	private bool GetTargetInRange(Vector3 direction, out HPComponent outHPComponent)
	{
		Ray ray = new Ray(transform.position, direction);
		Debug.DrawRay (ray.origin, ray.direction * m_range, Color.red);
		foreach (RaycastHit hit in Physics.RaycastAll(ray, m_range))
		{
			if (hit.collider.tag == "Ally") 
			{
				HPComponent hpComponent = hit.collider.GetComponent<HPComponent> ();
				if (hpComponent.isAlive) 
				{
					outHPComponent = hpComponent;
					return true;
				}
			}
		}

		//No targets in range
		outHPComponent = null;
		return false;
	}
}
