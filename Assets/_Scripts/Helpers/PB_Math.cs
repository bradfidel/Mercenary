using UnityEngine;

public class PB_Math : MonoBehaviour
{
    public static Vector2 RotateVector2ByAngle(Vector2 input, float angleInRadians)
    {
        return new Vector2(
            input.x * Mathf.Cos(angleInRadians) - input.y * Mathf.Sin(angleInRadians),
            input.x * Mathf.Sin(angleInRadians) + input.y * Mathf.Cos(angleInRadians)
            );
    }

    public static float VectorToAngle(Vector2 input)
    {
        return Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
    }
}
