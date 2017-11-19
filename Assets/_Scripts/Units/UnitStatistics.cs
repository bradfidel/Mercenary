using UnityEngine;

public sealed class UnitStatistics : MonoBehaviour
{
    public int initiative { get { return m_initiative; } }
    private int m_initiative;

    public void RollRandom()
    {
        m_initiative = Random.Range(1, 20);
    }
}
