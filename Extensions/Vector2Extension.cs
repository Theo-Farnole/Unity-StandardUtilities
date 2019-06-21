using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Extension 
{
    public static Vector3 ToXZ(this Vector2 v)
    {
        return new Vector3(v.x, 0, v.y);
    }
}
