using UnityEngine;

public class UnitAI : MonoBehaviour
{
    private Unit unit;

    private void Awake()
    {
        unit = GetComponent<Unit>();
        if (!unit.Possess(this))
        {
            Debug.LogError("Object already possessed, destroying this one");
            Destroy(this);
        }
    }
}
