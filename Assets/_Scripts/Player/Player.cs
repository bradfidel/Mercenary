using UnityEngine;
using UnityEngine.AI;

public class Player : UnitController
{
    private Unit m_controlledUnit;
    private NavMeshPath path;
    
    private void Awake()
    {
        path = new NavMeshPath();
    }

    private void Start()
    {
        GetRandomUnit();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveUnit();
        }

        CalculateMoveCost();
    }

    private void CalculateMoveCost()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
            NavMesh.CalculatePath(m_controlledUnit.transform.position, hit.point, NavMesh.AllAreas, path);

        float pathLength = 0;
        for (int i = 1; i < path.corners.Length; i++)
        {
            pathLength += (path.corners[i - 1] - path.corners[i]).magnitude;
            //Debug.DrawLine(path.corners[i - 1], path.corners[i], Color.cyan);
        }
        DebugCanvas.Display("Path length: " + pathLength, 0);
    }

    private void GetRandomUnit()
    {
        Unit[] unitsOnScene = FindObjectsOfType<Unit>();
        foreach (Unit unit in unitsOnScene)
        {
            if (unit.Possess(this))
            {
                m_controlledUnit = unit;
                break;
            }
        }

        if (!m_controlledUnit)
        {
            Debug.LogError("No units to controll. Destroying");
            Destroy(gameObject);
        }
    }

    private void MoveUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            //Debug.Log(hit.point);
            m_controlledUnit.MoveTo(hit.point);
        }
    }

    public override void TurnReceived()
    {
        Debug.Log("Player turn received");
    }

    [BitStrap.Button]
    public void EndTurnDebug()
    {
        m_controlledUnit.EndTurn();
    }
}
