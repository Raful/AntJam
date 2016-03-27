using UnityEngine;
using System.Collections;

public class BlissCloudShroom : AbstractShroom
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

        if (!gameObject.GetComponent<EventPlayer>().UpdateEventToPlay("event:/Player/Hurt"))
            Debug.Log("Could not change sound event.");
    }

    // Update is called once per frame
    void Update ()
    {
        m_CooldownTimer -= Time.deltaTime * 1000;
        if (m_CooldownTimer <= 0)
        {
            Debug.Log("Raycasting BCS.");
            Ray ray = new Ray(transform.position + Vector3.left * m_Range, Vector3.right);
            foreach (RaycastHit hit in Physics.RaycastAll(ray, m_Range * 2))
            {
                Debug.Log("Hit object with tag " + hit.collider.tag.ToString());
                if (hit.collider.tag == "Enemy")
                {
                    hit.collider.GetComponent<BlissComponent>().m_bliss.AddBliss(m_BlissInflict);
                    Debug.Log("Added " + m_BlissInflict.ToString() + " bliss to " + hit.collider.GetType().ToString() + " .");
                }
            }

            ray = new Ray(transform.position + Vector3.up * 20 - Vector3.left * m_Range, Vector3.right);
            foreach (RaycastHit hit in Physics.RaycastAll(ray, m_Range * 2))
            {
                if (hit.collider.tag == "Enemy")
                {
                    hit.collider.GetComponent<BlissComponent>().m_bliss.AddBliss(m_BlissInflict);
                }
            }

            m_CooldownTimer = m_AttackCooldown;
        }
    }

    [SerializeField]
    private int m_Range;
    [SerializeField]
    private int m_BlissInflict;
    [SerializeField][Tooltip("Cooldown in ms.")]
    private int m_AttackCooldown;
    private float m_CooldownTimer;
}
