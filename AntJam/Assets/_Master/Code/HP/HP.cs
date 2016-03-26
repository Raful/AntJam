[System.Serializable]
public class HP
{
    public int currentHp { get { return m_currentHp; } }
    public int maxHp { get { return m_maxHp; } }
    
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
    
    private int m_currentHp;
    [UnityEngine.SerializeField]
    private int m_maxHp;
}
