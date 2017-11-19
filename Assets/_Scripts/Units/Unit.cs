using UnityEngine;

public class Unit : MonoBehaviour
{
    private float m_changeTargetInterval = 10.3f;
    private float m_timer;

    private UnitMovementController m_movementController;
    public UnitMovementController movementController { get { return m_movementController; } }

    private Object m_owner = null;

    private bool m_inCombat = false;

    private void Awake()
    {
        m_movementController = GetComponent<UnitMovementController>();
    }

    private void OnEnable()
    {
        // temp -> later spawn system should call OnSpawn
        OnSpawn();
    }

    private void OnSpawn()
    {
        CombatSystem.RegisterUnit(this);
    }

    private void Update()
    {
        if (m_timer < Time.time && !m_owner && !m_inCombat)
        {
            m_timer = m_changeTargetInterval + Time.time;
            GoToRandomPlace();
        }
    }

    public bool Possess(Object newOwner)
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
        CombatSystem.RemoveUnit(this);
    }

    public void NotifyCombatStart()
    {
        Debug.LogError("TODO");
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

    private void StartTurn()
    {

    }
}
