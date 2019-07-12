using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyMath
{
    /// <summary>
    /// If int is greater than max, so int=max
    /// Else, if int is minor than min, so int=max 
    /// </summary>
    public static int InverseClamp(int i, int min, int max)
    {
        int value = i;

        if (value > max)
        {
            value = min;
        }

        if (value < min)
        {
            value = max;
        }

        return value;
    }
}
