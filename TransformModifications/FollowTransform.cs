using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    #region Fields
    [SerializeField] private Transform _transformToFollow;

    [SerializeField] private bool _followPosition = true;
    [SerializeField] private bool _followRotation = true;

    private Vector3 _offset = Vector3.zero;
    #endregion

    void Start()
    {
        if (_transformToFollow == null)
            return;

        _offset = transform.position - _transformToFollow.position;
    }

    void LateUpdate()
    {
        if (_transformToFollow == null)
            return;

        if (_followPosition)
        {
            transform.position = _transformToFollow.position + _offset;
        }

        if (_followRotation)
        {
            transform.rotation = _transformToFollow.rotation;
        }
    }
}
