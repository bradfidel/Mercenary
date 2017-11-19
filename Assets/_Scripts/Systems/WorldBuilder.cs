using UnityEngine;

public sealed class WorldBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject combatSystemPrefab;
    [SerializeField]
    private GameObject spawnSystemPrefab;

    private void Awake()
    {
        Instantiate(combatSystemPrefab, transform);
        Instantiate(spawnSystemPrefab, transform);
    }
}
