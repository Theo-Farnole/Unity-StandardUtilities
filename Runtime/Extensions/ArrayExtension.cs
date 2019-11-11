using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayExtension
{
    /// <summary>
    /// Return resized array if size is different from enum values length.
    /// </summary>
    /// <param name="enumType"></param>
    /// <returns>Type of an enum</returns>
    public static T[] ResizeIfNeeded<T>(this T[] array, Type enumType)
    {
        if (enumType.IsEnum == false)
        {
            Debug.LogWarning("enumType provided in paramater must be a enum type.");
            return array;
        }

        // resize if needed'
        if (array.Length != Enum.GetValues(enumType).Length)
        {
            Array.Resize(ref array, Enum.GetValues(enumType).Length);
        }

        return array;
    }
}
