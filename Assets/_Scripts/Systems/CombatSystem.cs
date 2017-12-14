using UnityEngine;
using System.Collections.Generic;

public class CombatSystem : MonoBehaviour
{
    private static bool debug = true;
    private static CombatSystem m_instance;
    public static CombatSystem Instance { get { return m_instance; } }

    private List<Unit> m_registeredUnits;
    private List<Unit> m_unitsWaitingForTurn;

    private bool m_combat = false;

    private void Awake()
    {
        m_registeredUnits = new List<Unit>();
        m_unitsWaitingForTurn = new List<Unit>();
        m_instance = this;
    }

    public void RegisterUnit(Unit unit)
    {
        m_registeredUnits.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        m_registeredUnits.Remove(unit);
        m_unitsWaitingForTurn.Remove(unit);
        if (debug) Debug.Log("[CombatSystem] Unit removed, count: " + m_registeredUnits.Count);
    }

    public void StartCombat(Unit initiatingUnit = null)
    {
        m_combat = true;
        foreach (Unit unit in m_registeredUnits)
            unit.NotifyCombatStart();
        if (debug) Debug.Log("[CombatSystem] Combat start.");

        PrepareUnitsList();
        StartNextTurn();
    }

    public void EndCombat(Unit initiatingUnit = null)
    {
        m_combat = false;
        foreach (Unit unit in m_registeredUnits)
            unit.NotifyCombatEnd();
        if (debug) Debug.Log("[CombatSystem] Combat end.");
    }


    private void PrepareUnitsList()
    {
        if (debug) Debug.LogError("PrepareUnitsList");
        for (int i = 0; i < m_registeredUnits.Count; i++)
            m_unitsWaitingForTurn.Add(m_registeredUnits[i]);

        m_unitsWaitingForTurn.Sort(delegate (Unit a, Unit b)
            {
                if (a.statistics.initiative > b.statistics.initiative)
                    return -1;
                else if (a.statistics.initiative < b.statistics.initiative)
                    return 1;
                else
                    return 0;
            });

        // TEMP list generation
        // TODO implement proper list generation when character system will be implemented

        if (m_unitsWaitingForTurn.Count == 0)
            EndCombat();
    }

    private void StartNextTurn()
    {
        Unit unit = m_unitsWaitingForTurn[0];
        Debug.LogError(unit.name + "'s initiative: " + unit.statistics.initiative);
        m_unitsWaitingForTurn.RemoveAt(0);
        unit.StartTurn();
    }

    public void UnitEndTurn(Unit unit)
    {
        PrepareForNextTurn();
    }

    private void PrepareForNextTurn()
    {
        if (m_unitsWaitingForTurn.Count == 0)
            PrepareUnitsList();
        if (m_combat)
            StartNextTurn();
    }

    #region debug
    [BitStrap.Button]
    public void StartCombatDebugButton()
    {
        StartCombat();
    }

    [BitStrap.Button]
    public void EndCombatDebugButton()
    {
        EndCombat();
    }
    #endregion
}
