using UnityEngine;
using System.Collections;



public class AntSpawner : MonoBehaviour
{
    [System.Serializable]
    private class SpawnableAnt
    {
        public GameObject antPrefab;
        [Tooltip("The probability this will be selected to spawn. Relative to other SpawnableAnts")]
        public float spawnProbability = 1;
        [Tooltip("How long before the spawner will consider spawning this")]
        public float timeToFirstSpawn;
    }

    [SerializeField]
    private float m_spawnCooldown = 1;

    [SerializeField]
    private SpawnableAnt[] m_antTypes;

    private float m_timeSinceLastSpawn = 0;
    private float m_timeSinceStart = 0;

	// Use this for initialization
	void Start ()
    {
	    if (m_antTypes.Length == 0)
        {
            Debug.LogError("No ant types are defined");
        }
	}

	// Update is called once per frame
	void Update ()
    {
	    if (m_timeSinceLastSpawn >= m_spawnCooldown)
        {
            m_timeSinceLastSpawn = 0;

            GameObject newAnt = Instantiate(GetRandomAntPrefab());
            newAnt.transform.position = transform.position;
            newAnt.transform.SetParent(transform, true);
        }

        m_timeSinceLastSpawn += Time.deltaTime;
        m_timeSinceStart += Time.deltaTime;

    }

    private GameObject GetRandomAntPrefab()
    {
        //Select an ant with roulette wheel selection
        float totalSpawnProbability = 0;
        foreach (SpawnableAnt antType in m_antTypes)
        {
            if (m_timeSinceStart >= antType.timeToFirstSpawn)
            {
                totalSpawnProbability += antType.spawnProbability;
            }
        }

        float randomSpawnValue = Random.Range(0, totalSpawnProbability);

        totalSpawnProbability = 0;
        foreach (SpawnableAnt antType in m_antTypes)
        {
            if (m_timeSinceStart >= antType.timeToFirstSpawn)
            {
                totalSpawnProbability += antType.spawnProbability;

                if (totalSpawnProbability >= randomSpawnValue)
                {
                    return antType.antPrefab;
                }
            }
        }

        //This should never happen
        return null;
    }
}
