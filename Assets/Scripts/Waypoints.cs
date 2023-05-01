using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public Transform[] waypoints;

    public Vector3 GetClosestPoint(Vector3 position)
    {
        float minDistance = Mathf.Infinity;
        Vector3 closestPoint = Vector3.zero;

        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Vector3 currentClosest = GetClosestPointOnSegment(waypoints[i].position, waypoints[i + 1].position, position);
            float distance = Vector3.Distance(position, currentClosest);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestPoint = currentClosest;
            }
        }

        return closestPoint;
    }

    private Vector3 GetClosestPointOnSegment(Vector3 a, Vector3 b, Vector3 point)
    {
        Vector3 segmentDirection = b - a;
        float segmentLengthSquared = segmentDirection.sqrMagnitude;
        Vector3 toPoint = point - a;

        float t = Vector3.Dot(toPoint, segmentDirection) / segmentLengthSquared;
        t = Mathf.Clamp01(t);

        return a + segmentDirection * t;
    }
}
