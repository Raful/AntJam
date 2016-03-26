[System.Serializable]
public class Bliss
{
    public int currentBliss { get { return m_currentBliss; } }
    public int maxBliss { get { return m_maxBliss; } }

    public void AddBliss(int bliss)
    {
        m_currentBliss += bliss;
        if (m_currentBliss > m_maxBliss)
            m_currentBliss = m_maxBliss;
    }

    public void RestoreBliss(int restoredBliss)
    {
        m_currentBliss -= restoredBliss;
        if (m_currentBliss < 0)
            m_currentBliss = 0;
    }

    private int m_currentBliss;
    [UnityEngine.SerializeField][UnityEngine.Tooltip("Bliss required to turn.")]
    private int m_maxBliss;
}
