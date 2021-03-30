using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class BoatController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float moveSpeed;
    public float turnSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            var isolationVector = new Vector3(1, 0, 1);
            var newPos = transform.position + Vector3.Scale(transform.forward, isolationVector).normalized * (moveSpeed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(newPos);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            var newRot = transform.rotation.eulerAngles - transform.up * (Time.fixedDeltaTime * turnSpeed);
            _rigidbody.MoveRotation(Quaternion.Euler(newRot));
        }

        if (Input.GetKey(KeyCode.D))
        {
            var newRot = transform.rotation.eulerAngles + transform.up * (Time.fixedDeltaTime * turnSpeed);
            _rigidbody.MoveRotation(Quaternion.Euler(newRot));
        }
    }
}
