using UnityEngine;

public sealed class UnitStatistics : MonoBehaviour
{
    public int initiative { get { return m_initiative; } }
    private int m_initiative;

    public float actionPoints { get { return m_actionPoints; } }
    private float m_actionPoints;

    public float speed { get { return m_speed; } }
    private float m_speed = 2.0f;  

    private int m_maxActionPoints;

    private void Awake()
    {
        GetComponent<Unit>().turnStart += OnTurnStart;
    }

    public void RollRandom()
    {
        m_initiative = Random.Range(1, 20);
        m_maxActionPoints = Random.Range(5, 11);
    }

    public void OnTurnStart()
    {
        m_actionPoints = m_maxActionPoints;
    }

    public void SpendActionPoints(float amount)
    {
        m_actionPoints -= amount;
        if(m_actionPoints < 0)
        {
            Debug.LogError("Unit used more action points than it could");
        }
    }
}
