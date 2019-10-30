using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DrawCircle))]
public class DrawCircleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DrawCircle myScript = (DrawCircle)target;
        if (GUILayout.Button("Draw circle"))
        {
            myScript.UpdateCircle();
        }
    }
}
