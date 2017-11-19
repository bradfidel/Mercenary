using UnityEngine;

public class Unit : MonoBehaviour
{
    private float m_changeTargetInterval = 10.3f;
    private float m_timer;
    private bool m_inCombat = false;

    private UnitMovementController m_movementController;
    public UnitMovementController movementController { get { return m_movementController; } }

    private UnitStatistics m_unitStatistics;
    public UnitStatistics unitStatistics { get { return m_unitStatistics; } }

    private UnitController m_owner = null;
    private CombatSystem m_combatSystem;

    

    private void Awake()
    {
        m_movementController = GetComponent<UnitMovementController>();
        m_unitStatistics = GetComponent<UnitStatistics>();
    }

    private void OnEnable()
    {
        // temp -> later spawn system should call OnSpawn
        OnSpawn();
        m_unitStatistics.RollRandom();
    }

    private void OnSpawn()
    {
        m_combatSystem = CombatSystem.Instance;
        m_combatSystem.RegisterUnit(this);
    }

    private void Update()
    {
        if (m_timer < Time.time && !m_owner && !m_inCombat)
        {
            m_timer = m_changeTargetInterval + Time.time;
            GoToRandomPlace();
        }
    }

    public bool Possess(UnitController newOwner)
    {
        if (m_owner)
        {
            // cannot possess
            return false;
        }
        else
        {
            m_owner = newOwner;
            return true;
        }
    }

    public bool Release(Object owner)
    {
        if (m_owner == owner)
        {
            m_owner = null;
            return true;
        }
        else
        {
            // cannot release, this unit belong to other owner
            return true;
        }
    }

    public void Death()
    {
        m_combatSystem.RemoveUnit(this);
    }

    public void NotifyCombatStart()
    {
        m_inCombat = true;
        movementController.Stop();
    }

    public void NotifyCombatEnd()
    {
        Debug.LogError("TODO");
        m_inCombat = false;
        movementController.Resume();
        GoToRandomPlace();
    }

    private void GoToRandomPlace()
    {
        m_movementController.MoveTo(new Vector3(
                   Random.Range(-50, 50),
                   0,
                   Random.Range(-50, 50)
                   ));
    }

    public void StartTurn()
    {
        if (m_owner != null)
        {
            m_owner.TurnReceived();
        }
        else
        {
            EndTurn();
        }
    }

    public void EndTurn()
    {
        m_combatSystem.UnitEndTurn(this);
    }
}
