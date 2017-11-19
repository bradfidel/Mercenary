using UnityEngine;

public class UnitAI : UnitController
{
    private Unit m_controlledUnit;

    public override void TurnReceived()
    {
        Debug.Log(m_controlledUnit + " controlled by bot. Passing turn.");
        m_controlledUnit.EndTurn();
    }

    private void Awake()
    {
        m_controlledUnit = GetComponent<Unit>();
        if (!m_controlledUnit.Possess(this))
        {
            Debug.LogError("Object already possessed, destroying this one");
            Destroy(this);
        }
    }
}
