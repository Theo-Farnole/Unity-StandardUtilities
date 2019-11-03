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
        public static IEnumerable<Type> GetSubclass<T>(Assembly assembly)
        {
            Type parentType = typeof(T);
            Type[] types = assembly.GetTypes();

            IEnumerable<Type> subclasses = types.Where(t => t.IsSubclassOf(parentType));

            return subclasses;
        }

        public static IEnumerable<Type> GetSubclass<T>()
        {
            Assembly assembly = typeof(T).Assembly;

            return GetSubclass<T>(assembly);
        }

        public static Assembly GetAssemblyByName(string name)
        {
            return AppDomain.CurrentDomain.GetAssemblies().
                   SingleOrDefault(assembly => assembly.GetName().Name == name);
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

            UnityEngine.Gizmos.DrawLine(offset + new Vector3(r.min.x, r.max.y), offset + new Vector3(r.max.x, r.max.y));
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
}
