using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class BoatController : MonoBehaviour, IBoatController
{
    
    [FormerlySerializedAs("maxSpeed")] public float maxVelocity;
    public float acceleration;
    private float _velocity;
    
    [FormerlySerializedAs("turnSpeed")] public float maxTurnSpeed;
    [FormerlySerializedAs("_turnAccelerationFactor")] public float turnAccelerationFactor;
    public float turnDecelerationFactor;
    private float _currentTurnSpeed;
    
    private bool _isPlayerDriving = false;
    public GameObject playerDriving;
    
    [FormerlySerializedAs("camera")] public Camera boatCamera;
    
    private Rigidbody _rigidbody;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _velocity = 0;
        _currentTurnSpeed = 0;
    }

    private void FixedUpdate()
    {
        if (!_isPlayerDriving) return;

        if (Input.GetButtonDown("Interact"))
        {
            _isPlayerDriving = false;
            playerDriving.transform.position = transform.position + 5 * transform.right;
            boatCamera.gameObject.SetActive(false);
            playerDriving.SetActive(true);
        }
        
        _velocity = Input.GetKey(KeyCode.W) ? Mathf.Min(_velocity + acceleration * Time.fixedDeltaTime, maxVelocity) : Mathf.Lerp(_velocity, 0, 0.02f);

        var forwardNotVertical = transform.forward;
        forwardNotVertical.y = 0;
        forwardNotVertical.Normalize();
        var newPos = transform.position + forwardNotVertical * (_velocity * Time.fixedDeltaTime);
        _rigidbody.MovePosition(newPos);
        
        if (Input.GetKey(KeyCode.A))
        {
            _currentTurnSpeed = Mathf.Lerp(_currentTurnSpeed, -maxTurnSpeed, turnAccelerationFactor * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.D))
        {
            _currentTurnSpeed = Mathf.Lerp(_currentTurnSpeed, maxTurnSpeed, turnAccelerationFactor * Time.deltaTime);
        }
        else
        {
            _currentTurnSpeed = Mathf.Lerp(_currentTurnSpeed, 0, turnDecelerationFactor * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Equals))
        {
            maxVelocity += 5;
        }

        if (Input.GetKeyDown(KeyCode.Minus))
        {
            maxVelocity -= 5;
        }

        var newRot = transform.rotation.eulerAngles + transform.up * (Time.fixedDeltaTime * _currentTurnSpeed);
        _rigidbody.MoveRotation(Quaternion.Euler(newRot));
    }

    public bool IsPlayerDriving()
    {
        return _isPlayerDriving;
    }

    public void GetInBoat(GameObject player)
    {
        playerDriving = player;
        player.SetActive(false);
        boatCamera.gameObject.SetActive(true);
        _isPlayerDriving = true;
    }
}
