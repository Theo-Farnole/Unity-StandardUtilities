using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allow draw debug axis in the SceneView.
/// </summary>
public class GizmosDrawRelativeAxis : MonoBehaviour
{
    [System.Serializable]
    struct Axis
    {
        [SerializeField] private bool _draw;
        [SerializeField] private Color _drawColor;

        public Axis(bool draw, Color drawColor)
        {
            _draw = draw;
            _drawColor = drawColor;
        }

        public void DrawAxis(Vector3 origin, Vector3 direction, float length = 1)
        {
            if (_draw)
            {
                DrawArrow.ForGizmo(origin, direction * length, _drawColor);                
                DrawArrow.ForGizmo(origin, direction * length, _drawColor);                
            }
        }
    }

    [SerializeField] private bool _drawOnSelectedOnly = false;
    [SerializeField] private float _axisLength = 1;
    [SerializeField] private Vector3 _offset;
    [Header("AXIS")]
    [SerializeField] private Axis _drawUpAxis = new Axis(true, Color.blue);
    [SerializeField] private Axis _drawDownAxis = new Axis(true, Color.blue);
    [SerializeField] private Axis _drawForwardAxis = new Axis(true, Color.blue);
    [SerializeField] private Axis _drawBackwardAxis = new Axis(true, Color.blue);
    [SerializeField] private Axis _drawRightAxis = new Axis(true, Color.blue);
    [SerializeField] private Axis _drawLeftAxis = new Axis(true, Color.blue);

    void OnDrawGizmos()
    {
        DrawAxis();    
    }

    public void DrawAxis()
    {
        // this method is called from custom editor

        bool isSelected = UnityEditor.Selection.activeGameObject == gameObject;

        if (_drawOnSelectedOnly && isSelected || !_drawOnSelectedOnly)
        {
            Debug.LogFormat("Je draw");

            Vector3 rootPosition = transform.position; // + _offset

            _drawUpAxis.DrawAxis(rootPosition, transform.up, _axisLength);
            _drawDownAxis.DrawAxis(rootPosition, -transform.up, _axisLength);
            _drawForwardAxis.DrawAxis(rootPosition, transform.forward, _axisLength);
            _drawBackwardAxis.DrawAxis(rootPosition, -transform.forward, _axisLength);
            _drawRightAxis.DrawAxis(rootPosition, transform.right, _axisLength);
            _drawLeftAxis.DrawAxis(rootPosition, -transform.right, _axisLength);
        }
    }
}
