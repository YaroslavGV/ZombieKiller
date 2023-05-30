using UnityEngine;

public static class GizmosUtility
{
    private const float RingSegmentLenght = 0.25f;

    /// <param name="segment"> 0 is auto </param>
    public static void DrawRingXY (Vector3 center, float radius, int segment = 0)
    {
        if (segment <= 0)
        {
            float lenght = Mathf.PI * 2f * radius;
            segment = Mathf.RoundToInt(lenght / RingSegmentLenght);
        }
        if (segment < 0)
            segment = Mathf.Abs(segment);

        Vector3[] points = new Vector3[segment];
        float radianStep = Mathf.PI * 2f / (float)segment;
        for (int i = 0; i < segment; i++)
        {
            float radian = i * radianStep;
            points[i] = center + new Vector3(Mathf.Cos(radian), Mathf.Sin(radian)) * radius;
        }
        for (int i = 0; i < segment - 1; i++)
            Gizmos.DrawLine(points[i], points[i + 1]);
        Gizmos.DrawLine(points[segment - 1], points[0]);
    }

    public static void DrawRectXY (Vector3 center, Vector2 size)
    {
        Vector2 halfSize = size * 0.5f;
        Vector3[] points =
        {
                center + new Vector3(halfSize.x, halfSize.y),
                center + new Vector3(-halfSize.x, halfSize.y),
                center + new Vector3(-halfSize.x, -halfSize.y),
                center + new Vector3(halfSize.x, -halfSize.y),
            };
        for (int i = 0; i < 3; i++)
            Gizmos.DrawLine(points[i], points[i + 1]);
        Gizmos.DrawLine(points[3], points[0]);
    }
}
