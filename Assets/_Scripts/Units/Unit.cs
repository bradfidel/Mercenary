using UnityEngine;

public class Unit : MonoBehaviour
{
    //private float m_changeTargetInterval = 10.3f;
    private float m_timer;
    private bool m_inCombat = false;
    public bool inCombat { get { return m_inCombat; } }

    public bool hasTurn { get { return m_hasTurn; } }
    private bool m_hasTurn = true;

    private UnitMovementController m_movementController;
    public UnitMovementController movementController { get { return m_movementController; } }

    private UnitStatistics m_statistics;
    public UnitStatistics statistics { get { return m_statistics; } }

    private UnitController m_owner = null;
    private CombatSystem m_combatSystem;

    public delegate void TurnStart();
    public event TurnStart turnStart;

    // TODO store callback with currently ongoing action - to start another one previous must finish first

    private void Awake()
    {
        m_movementController = GetComponent<UnitMovementController>();
        m_statistics = GetComponent<UnitStatistics>();
    }

    private void OnEnable()
    {
        // temp -> later spawn system should call OnSpawn
        OnSpawn();
        m_statistics.RollRandom();
    }

    private void OnSpawn()
    {
        m_combatSystem = CombatSystem.Instance;
        m_combatSystem.RegisterUnit(this);
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
    }

    public void StartTurn()
    {
        turnStart();
        if (m_owner != null)
        {
            m_owner.TurnReceived();
        }
        else
        {
            EndTurn();
        }
        m_hasTurn = true;
    }

    public void EndTurn()
    {
        m_hasTurn = false;
        m_combatSystem.UnitEndTurn(this);
    }

    public void MoveTo(Vector3 destination)
    {
        //float movementCost = NavMeshHelper.GetPathMovementCost(transform.position, destination, statistics.speed);
        //Debug.Log(movementCost);
        movementController.MoveTo(destination, m_inCombat ? statistics.actionPoints : 222222); // random big number, Mathf.Inf generate problems
    }
}
