using UnityEngine;

public class Player : UnitController
{
    private Unit m_controlledUnit;

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
            m_controlledUnit.movementController.MoveTo(hit.point);
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
