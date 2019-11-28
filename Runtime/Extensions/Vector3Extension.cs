using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extension
{
    public static void Clamp(this Vector3 v, float min, float max)
    {
        v.x = Mathf.Clamp(v.x, min, max);
        v.y = Mathf.Clamp(v.y, min, max);
        v.z = Mathf.Clamp(v.z, min, max);
    }

    public static void Clamp(this Vector3 v1, Vector3 min, Vector3 max)
    {
        v1.x = Mathf.Clamp(v1.x, min.x, max.x);
        v1.y = Mathf.Clamp(v1.y, min.y, max.y);
        v1.z = Mathf.Clamp(v1.z, min.z, max.z);
    }

    public static Vector3 GetClosestPoint(this Vector3 currentPosition, Vector3[] points)
    {
        Vector3 closestPoint = Vector3.one * Mathf.Infinity;
        float minimunDistance = Mathf.Infinity;

        for (int i = 0; i < points.Length; i++)
        {
            float distance = Vector3.Distance(points[i], currentPosition);

            if (distance < minimunDistance)
            {
                closestPoint = points[i];
                minimunDistance = distance;
            }
        }
        return closestPoint;
    }

    public static Vector3 SetX(this Vector3 v, float value)
    {
        v[0] = value;
        return v;
    }

    public static Vector3 SetY(this Vector3 v, float value)
    {
        v[1] = value;
        return v;
    }

    public static Vector3 SetZ(this Vector3 v, float value)
    {
        v[2] = value;
        return v;
    }
}
