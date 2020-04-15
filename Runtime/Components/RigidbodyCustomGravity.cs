using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Apply a gravity scale to a rigidbody.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class RigidbodyGravityScale : MonoBehaviour
{
    private float _gravityScale = 1;

    private Rigidbody _rigidbody;

    public float GravityScale { get => _gravityScale; set => _gravityScale = value; }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false; // we apply gravity manually
    }

    void FixedUpdate()
    {
        UnityEngine.Assertions.Assert.IsFalse(_rigidbody.useGravity, "If applying a custom gravity scale, don't tick \"use gravity\" property.");

        // apply gravity        
        Vector3 gravityScaled = Physics.gravity * _gravityScale;
        _rigidbody.AddForce(gravityScaled, ForceMode.Acceleration);
    }
}
