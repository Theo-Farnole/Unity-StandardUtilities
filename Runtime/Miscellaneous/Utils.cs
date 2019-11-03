using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Utils
{
    public static class UtilsClass
    {
        public static IEnumerable<Type> GetSubclass<T>()
        {
            Type parentType = typeof(T);
            Assembly assembly = typeof(T).Assembly;
            Type[] types = assembly.GetTypes();

            IEnumerable<Type> subclasses = types.Where(t => t.IsSubclassOf(parentType));

            return subclasses;
        }

        public static void CaptureCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public static void ReleaseCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
