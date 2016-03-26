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

    void OnTriggerStay(Collider unit)
    {
        if (unit.tag == "Enemy" && !unit.GetComponent<AntMovement>().isHalted)
        {
            float slow = this.GetComponentInParent<GoopShroom>().SlowForce / 100;
            Vector3 speedReduction = Vector3.right * slow * unit.GetComponent<AntMovement>().Speed * Time.deltaTime;
            unit.gameObject.transform.Translate(speedReduction, Space.Self);
        }
    }

    private int m_auraRange;
}
