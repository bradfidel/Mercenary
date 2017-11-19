using UnityEngine;
using UnityEngine.AI;

public class UnitMovementController : MonoBehaviour
{
    private NavMeshAgent m_navAgent;

    private void Awake()
    {
        m_navAgent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 target)
    {
        m_navAgent.SetDestination(target);
    }

    public void Stop()
    {
        m_navAgent.isStopped = true;
    }

    public void Resume()
    {
        m_navAgent.isStopped = false;
    }
}
