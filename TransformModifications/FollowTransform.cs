using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    #region Fields
    [SerializeField] private Transform _transformToFollow;

    [SerializeField] private bool _followPosition = true;
    [SerializeField] private bool _followRotation = true;
    #endregion

    void Update()
    {
        if (_transformToFollow == null)
            return;

        if (_followPosition)
        {
            transform.position = _transformToFollow.position;
        }

        if (_followRotation)
        {
            transform.rotation = _transformToFollow.rotation;
        }
    }
}
