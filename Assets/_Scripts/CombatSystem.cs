using UnityEngine;
using System.Collections.Generic;

public class CombatSystem : MonoBehaviour
{
    private static bool debug = true;
    private static CombatSystem m_instance;

    private List<Unit> m_registeredUnits;

    private void Awake()
    {
        m_registeredUnits = new List<Unit>();
        m_instance = this;
    }

    public static void RegisterUnit(Unit unit)
    {
        m_instance.m_registeredUnits.Add(unit);
        if (debug) Debug.Log("[CombatSystem] Unit registered, count: " + m_instance.m_registeredUnits.Count);
    }

    public static void RemoveUnit(Unit unit)
    {
        m_instance.m_registeredUnits.Remove(unit);
        if (debug) Debug.Log("[CombatSystem] Unit removed, count: " + m_instance.m_registeredUnits.Count);
    }

    public void StartCombat(Unit initiatingUnit = null)
    {
        foreach (Unit unit in m_registeredUnits)
            unit.NotifyCombatStart();
        if (debug) Debug.Log("[CombatSystem] Combat start.");
    }

    public void EndCombat(Unit initiatingUnit = null)
    {
        foreach (Unit unit in m_registeredUnits)
            unit.NotifyCombatEnd();
        if (debug) Debug.Log("[CombatSystem] Combat end.");
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
