using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{
    /// <summary>
    /// Returns nearest transform from transforms args.
    /// </summary>
    /// <param name="t"></param>
    /// <param name="transforms">List of transforms to check who's the nearest</param>
    /// <returns>Nearest Transform</returns>
    /// <author> 
    /// code from https://forum.unity.com/threads/clean-est-way-to-find-nearest-object-of-many-c.44315/
    /// </author>
    public static Transform GetClosestTransform(this Transform t, Transform[] transforms)
    {
        Vector3 currentPos = t.position;

        Transform tMin = null;
        float minDist = Mathf.Infinity;

        foreach (Transform e in transforms)
        {
            float dist = Vector3.Distance(e.position, currentPos);

            if (dist < minDist)
            {
                tMin = e;
                minDist = dist;
            }
        }
        return tMin;
    }

    /// <summary>
    /// Returns nearest transform from colliders args.
    /// </summary>
    /// <param name="t"></param>
    /// <param name="colliders">List of colliders to check who's the nearest</param>
    /// <returns></returns>
    public static Transform GetClosestTransform(this Transform t, Collider[] colliders)
    {
        var transforms = new Transform[colliders.Length];

        for (int i = 0; i < transforms.Length; i++)
        {
            transforms[i] = colliders[i].transform;
        }

        return GetClosestTransform(t, transforms);
    }
}
