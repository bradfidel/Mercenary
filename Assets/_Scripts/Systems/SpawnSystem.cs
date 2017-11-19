using UnityEngine;

public sealed class SpawnSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject prototypePrefab;
    [SerializeField]
    private GameObject playerPrefab;

    private Transform m_unitsContainer;


    private void Awake()
    {
        m_unitsContainer = new GameObject("Units Container").transform;
    }

    private void Start()
    {
        InitialSpawn(5);
    }

    private GameObject Spawn(GameObject prefab, Vector3 position)
    {
        GameObject go = Instantiate(prefab, position, Quaternion.identity, m_unitsContainer);
        return go;
    }

    #region debug
    [BitStrap.Button]
    public void DebugSpawnUnit()
    {
        Spawn(prototypePrefab, new Vector3(Random.Range(-25, 25), 0, Random.Range(-25, 25)));
    }

    [BitStrap.Button]
    public void DebugSpawnPlayer()
    {
        /* Player player = */ Spawn(playerPrefab, Vector3.zero).GetComponent<Player>();
    }

    private void InitialSpawn(int unitsCount)
    {
        for (int i = 0; i < unitsCount; i++)
            DebugSpawnUnit();

        if (unitsCount > 0)
            DebugSpawnPlayer();
    }
#endregion
}
