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

    public static void Clamp(this Vector3 v1, Vector3 v2)
    {
        v1.x = Mathf.Clamp(v1.x, -Mathf.Abs(v2.x), Mathf.Abs(v2.x));
        v1.y = Mathf.Clamp(v1.y, -Mathf.Abs(v2.y), Mathf.Abs(v2.y));
        v1.z = Mathf.Clamp(v1.z, -Mathf.Abs(v2.z), Mathf.Abs(v2.z));
    }

    public static Vector3 GetClosestVector(this Vector3 currentPos, Vector3[] vectorsToCheck)
    {
        Vector3 closestVector = Vector3.one * Mathf.Infinity;
        float minDist = Mathf.Infinity;

        for (int i = 0; i < vectorsToCheck.Length; i++)
        {
            float dist = Vector3.Distance(vectorsToCheck[i], currentPos);

            if (dist < minDist)
            {
                closestVector = vectorsToCheck[i];
                minDist = dist;
            }
        }
        return closestVector;
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
