using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lortedo.Utilities
{
    public class LookAtCamera : MonoBehaviour
    {
        Camera _mainCamera;

        void Awake()
        {
            _mainCamera = Camera.main;
        }

        void Update()
        {
            transform.LookAt(_mainCamera.transform);
        }
    }
}
