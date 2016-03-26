using UnityEngine;
using System.Collections;

public class GoopAura : MonoBehaviour
{

	// Use this for initialization
	void OnEnable ()
    {
        Debug.Log("Slow aura enabled.");
        m_auraRange = this.gameObject.GetComponentInParent<GoopShroom>().Range;
        this.gameObject.GetComponent<BoxCollider>().size = new Vector3(-m_auraRange, 10, m_auraRange);
        Vector3 boxCol = this.GetComponent<BoxCollider>().size;
        Debug.Log("Box collider size: " + boxCol.ToString());
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider unit)
    {
        if (unit.tag == "Enemy")
        {
            Debug.Log("Enemy entered slow.");
            // Apply slow
        }
    }

    void OnTriggerExit(Collider unit)
    {
        if (unit.tag == "Enemy")
        {
            Debug.Log("Enemy left the slow.");
            //Remove slow
        }
    }

    private int m_auraRange;
}
