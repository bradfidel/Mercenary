using UnityEngine;
using UnityEngine.AI;

public sealed class NavMeshHelper
{
    public static float GetPathMovementCost(Vector3 from, Vector3 to, float unitSpeed)
    {
        NavMeshPath path = new NavMeshPath();
        NavMesh.CalculatePath(from, to, NavMesh.AllAreas, path);
        float pathLength = 0;
        for (int i = 1; i < path.corners.Length; i++)
        {
            float currentLineLength = (path.corners[i - 1] - path.corners[i]).magnitude;
            pathLength += currentLineLength;
        }

        return pathLength;
    }
}
