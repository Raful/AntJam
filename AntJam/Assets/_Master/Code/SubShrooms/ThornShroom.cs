using UnityEngine;
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
                        hit.collider.GetComponent<HPComponent>().isHurt = true;
                    }
                }
            }
            // Reset cooldown
            m_CooldownTimer = m_AttackCooldown;
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
    private int m_Damage;
    [SerializeField]
    private int m_AttackCooldown;
    [SerializeField]
    private int m_Range;
    private float m_CooldownTimer;
}
