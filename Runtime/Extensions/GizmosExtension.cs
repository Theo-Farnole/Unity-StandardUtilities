using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GizmosExtension
{
    /// <summary>
    /// Draw Rect in with Gizmos line.
    /// </summary>
    public static void DrawWirefameRect(Rect r)
    {
        DrawWireframeRect(r, Vector3.zero);
    }

    /// <summary>
    /// Draw Rect in with Gizmos line.
    /// </summary>
    public static void DrawWireframeRect(Rect r, Vector3 offset)
    {
        offset.z = 0;

        Gizmos.DrawLine(offset + new Vector3(r.min.x, r.max.y), offset + new Vector3(r.max.x, r.max.y));
        Gizmos.DrawLine(offset + new Vector3(r.min.x, r.min.y), offset + new Vector3(r.max.x, r.min.y));
        Gizmos.DrawLine(offset + new Vector3(r.min.x, r.min.y), offset + new Vector3(r.min.x, r.max.y));
        Gizmos.DrawLine(offset + new Vector3(r.max.x, r.min.y), offset + new Vector3(r.max.x, r.max.y));
    }

    /// <summary>
    /// Draw gizmos line without Z axis.
    /// </summary>
    public static void DrawLine2D(Vector2 from, Vector2 to)
    {
        Gizmos.DrawLine(from, to);
    }
}
