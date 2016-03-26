[System.Serializable]
public class HP
{
    public int currentHp { get { Init(); return m_currentHp; } }
    public int maxHp { get { Init(); return m_maxHp; } }

    public HP()
    {
        m_currentHp = m_maxHp;
    }
    
    public void DealDamage(int damage)
    {
        m_currentHp -= damage;

        if (currentHp < 0)
        {
            m_currentHp = 0;
        }
    }

    public void RestoreDamage(int restoredDamage)
    {
        m_currentHp += restoredDamage;

        if (currentHp > maxHp)
        {
            m_currentHp = maxHp;
        }
    }

    private void Init()
    {
        if (!m_isInitialized)
        {
            m_isInitialized = true;
            m_currentHp = m_maxHp;
        }
    }

    [UnityEngine.SerializeField]
    private int m_maxHp;
    private int m_currentHp;

    private bool m_isInitialized = false;
}
