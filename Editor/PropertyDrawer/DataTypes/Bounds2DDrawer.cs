namespace Lortedo.Utilities
{
    using UnityEngine;
    using UnityEditor;
    using Lortedo.Utilities.DataTypes;
    using UnityEngine.Assertions;

    [CustomPropertyDrawer(typeof(Bounds2D))]
    public class Bounds2DDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty minimum = property.FindPropertyRelative("min");
            SerializedProperty maximum = property.FindPropertyRelative("max");

            Assert.IsNotNull(minimum, "Property 'min' not found in Bounds2D.");
            Assert.IsNotNull(maximum, "Property 'max' not found in Bounds2D.");


            GUILayoutOption[] options = { GUILayout.MaxWidth(100.0f), GUILayout.MinWidth(10.0f) };

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PrefixLabel(label);

            EditorGUILayout.LabelField("Min", options);
            minimum.floatValue = EditorGUILayout.FloatField(minimum.floatValue);
            EditorGUILayout.LabelField("Max", options);
            maximum.floatValue = EditorGUILayout.FloatField(maximum.floatValue);


            EditorGUILayout.EndHorizontal();            
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 0;
        }
    }
}