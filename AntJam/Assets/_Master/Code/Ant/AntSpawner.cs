using UnityEngine;
using System.Collections;

public class AntSpawner : MonoBehaviour
{
    [SerializeField]
    private float m_spawnCooldown = 1;

    [SerializeField]
    private GameObject m_antPrefab;

    private float m_timeSinceLastSpawn = 0;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (m_timeSinceLastSpawn >= m_spawnCooldown)
        {
            m_timeSinceLastSpawn = 0;

            GameObject newAnt = Instantiate(m_antPrefab);
            newAnt.transform.position = transform.position;
            newAnt.transform.SetParent(transform, true);
        }

        m_timeSinceLastSpawn += Time.deltaTime;
	}
}
