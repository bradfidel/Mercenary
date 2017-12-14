using UnityEngine;
using UnityEngine.AI;

public class UnitMovementController : MonoBehaviour
{
    private NavMeshAgent m_navAgent;
    private NavMeshPath path;
    private UnitStatistics stats;
    public bool isMoving { get { return m_navAgent.velocity.magnitude >= 0.05f; } }

    private void Awake()
    {
        m_navAgent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        stats = GetComponent<UnitStatistics>();
    }

    private void Update()
    {
        for (int i = 1; i < path.corners.Length; i++)
        {
            Debug.DrawLine(path.corners[i - 1], path.corners[i], Color.cyan);
        }
    }

    public void MoveTo(Vector3 destination, float allowedActionPoints)
    {
        if (isMoving)
            return;

        NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);
        float pathLength = 0;
        float actionPointsLeft = allowedActionPoints;
        for (int i = 1; i < path.corners.Length; i++)
        {
            float currentLineLength = (path.corners[i - 1] - path.corners[i]).magnitude;
            pathLength += currentLineLength;
            float possibleMovementDistance = actionPointsLeft * stats.speed;

            if (currentLineLength > possibleMovementDistance)
            {
                // calc how much can unit go
                Vector3 diff = path.corners[i] - path.corners[i - 1];
                float availablePartRatio = possibleMovementDistance / diff.magnitude;   // TODO apLeft * movementSpeed
                Vector3 newDestination = path.corners[i - 1] + diff * availablePartRatio;
                destination = newDestination;
                actionPointsLeft = 0;
                break;
            }
            else
            {
                actionPointsLeft -= currentLineLength / stats.speed;
            }
            Debug.DrawLine(path.corners[i - 1], path.corners[i], Color.cyan);
        }

        // TODO event/callback on movement finished

        Resume();
        m_navAgent.SetDestination(destination);
        stats.SpendActionPoints(allowedActionPoints - actionPointsLeft);
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
