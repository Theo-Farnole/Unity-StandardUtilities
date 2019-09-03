using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FancyObject : MonoBehaviour
{
    #region Fields
    [Header ("Rotation")]
    [SerializeField] private bool _shouldRotate;
    [Space]
    [SerializeField] private Vector3 _rotateSpeed;

    [Header("Translate on Y axis")]
    [SerializeField] private bool _shouldTranslate;
    [Space]
    [SerializeField] private float _translateSpeed;
    [SerializeField] private float _translateDistance;

    private Vector3 _startingPosition;
    #endregion

    #region Methods
    void Start()
    {
        _startingPosition = transform.localPosition;
    }

    void FixedUpdate()
    {
        if (_shouldRotate)
        {
            transform.eulerAngles += _rotateSpeed * Time.fixedDeltaTime;
        }

        if (_shouldTranslate)
        {
            var pos = Vector3.zero;
            pos[1] = Mathf.Sin(_translateSpeed * Time.time) * _translateDistance;
            transform.localPosition = _startingPosition + pos;
        }
    }

    public void ResetStartingPosition(Vector3 position)
    {
        transform.position = position;
        _startingPosition = position;
    }
    #endregion
}
